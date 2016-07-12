﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library
{
    // Undirected, unweighted graph with no loops or multiple edges: http://mathworld.wolfram.com/SimpleGraph.html.
    // The graph's vertices are stored in an array and the ID of a vertex (from 0 to vertexCount - 1)
    // corresponds to its index in said array. Immutable so far but at least mutable edges later on probably.
    // Not bothering to throw exceptions in the case where vertices from other graphs are passed in.
    public class SimpleGraph
    {
        protected readonly Vertex[] _vertices;

        protected SimpleGraph(int vertexCount)
        {
            _vertices = new Vertex[vertexCount];
        }

        // For example, edges like (0, 1), (1, 2) => there's an edge between vertices 0 and 1 and 1 and 2.
        public static SimpleGraph Create(int vertexCount, int[,] edges)
        {
            var graph = new SimpleGraph(vertexCount);

            for (int id = 0; id < vertexCount; ++id)
            {
                graph._vertices[id] = new Vertex(graph, id);
            }

            for (int i = 0; i < edges.GetLength(0); ++i)
            {
                graph.AddEdge(edges[i, 0], edges[i, 1]);
            }

            return graph;
        }

        public int VertexCount
            => _vertices.Length;

        public IReadOnlyList<Vertex> Vertices
            => Array.AsReadOnly(_vertices);

        protected void AddEdge(int firstVertexID, int secondVertexID)
            => AddEdge(_vertices[firstVertexID], _vertices[secondVertexID]);

        protected void AddEdge(Vertex firstVertex, Vertex secondVertex)
        {
            firstVertex.AddNeighbor(secondVertex);
            secondVertex.AddNeighbor(firstVertex);
        }

        public bool HasEdge(int firstVertexID, int secondVertexID)
            => HasEdge(_vertices[firstVertexID], _vertices[secondVertexID]);

        public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
            => firstVertex.HasNeighbor(secondVertex);

        // This performs a DFS from an arbitrary start vertex, to determine if the whole graph is reachable from it.
        public bool IsConnected()
        {
            var arbitraryStartVertex = _vertices[VertexCount / 2];
            var discoveredVertexIDs = new HashSet<int> { arbitraryStartVertex.ID };
            var verticesToVisit = new Stack<Vertex>();
            verticesToVisit.Push(arbitraryStartVertex);

            while (verticesToVisit.Count > 0)
            {
                var vertex = verticesToVisit.Pop();

                foreach (var neighbor in vertex.Neighbors)
                {
                    bool neighborWasJustDiscovered = discoveredVertexIDs.Add(neighbor.ID);
                    if (neighborWasJustDiscovered)
                    {
                        verticesToVisit.Push(neighbor);
                    }
                }
            }

            return discoveredVertexIDs.Count == VertexCount;
        }

        public class Vertex
        {
            protected readonly SimpleGraph _graph;
            protected readonly HashSet<Vertex> _neighbors = new HashSet<Vertex>();

            protected internal Vertex(SimpleGraph graph, int ID)
            {
                _graph = graph;
                this.ID = ID;
            }

            public int ID { get; }

            public int Degree
                => _neighbors.Count;

            public IEnumerable<Vertex> Neighbors
                => _neighbors.Skip(0);

            protected internal void AddNeighbor(int neighborID)
                => AddNeighbor(_graph._vertices[neighborID]);

            protected internal void AddNeighbor(Vertex neighbor)
                => _neighbors.Add(neighbor);

            public bool HasNeighbor(int neighborID)
                => HasNeighbor(_graph._vertices[neighborID]);

            public bool HasNeighbor(Vertex neighbor)
                => _neighbors.Contains(neighbor);
        }
    }
}
