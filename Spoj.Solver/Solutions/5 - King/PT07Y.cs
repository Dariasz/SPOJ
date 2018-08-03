﻿using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/PT07Y/ #graph-theory #tree
// Determines if the given graph is a tree.
public static class PT07Y
{
    // Skipping the details, a graph is a tree if it's connected and has (vertexCount - 1) edges.
    public static bool Solve(int vertexCount, int edgeCount, int[,] edges)
    {
        if (edgeCount != vertexCount - 1)
            return false;

        return SimpleGraph
            .CreateFromOneBasedEdges(vertexCount, edges)
            .IsConnected();
    }
}

// Undirected, unweighted graph with no loops or multiple edges: http://mathworld.wolfram.com/SimpleGraph.html.
// The graph's vertices are stored in an array and the ID of a vertex (from 0 to vertexCount - 1) corresponds to
// its index in that array.
public sealed class SimpleGraph
{
    public SimpleGraph(int vertexCount)
    {
        var vertices = new Vertex[vertexCount];
        for (int id = 0; id < vertexCount; ++id)
        {
            vertices[id] = new Vertex(this, id);
        }

        Vertices = Array.AsReadOnly(vertices);
    }

    // For example, edges like (1, 2), (2, 3) => there's an edge between vertices 0 and 1 and 1 and 2.
    public static SimpleGraph CreateFromOneBasedEdges(int vertexCount, int[,] edges)
    {
        var graph = new SimpleGraph(vertexCount);
        for (int i = 0; i < edges.GetLength(0); ++i)
        {
            graph.AddEdge(edges[i, 0] - 1, edges[i, 1] - 1);
        }

        return graph;
    }

    public IReadOnlyList<Vertex> Vertices { get; }
    public int VertexCount => Vertices.Count;

    public void AddEdge(int firstVertexID, int secondVertexID)
        => AddEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public void AddEdge(Vertex firstVertex, Vertex secondVertex)
    {
        firstVertex.AddNeighbor(secondVertex);
        secondVertex.AddNeighbor(firstVertex);
    }

    public bool HasEdge(int firstVertexID, int secondVertexID)
        => HasEdge(Vertices[firstVertexID], Vertices[secondVertexID]);

    public bool HasEdge(Vertex firstVertex, Vertex secondVertex)
        => firstVertex.HasNeighbor(secondVertex);

    // This performs a DFS from an arbitrary start vertex, to determine if the whole graph is reachable from it.
    public bool IsConnected()
    {
        var arbitraryStartVertex = Vertices[VertexCount / 2];
        var discoveredVertexIDs = new HashSet<int> { arbitraryStartVertex.ID };
        var verticesToVisit = new Stack<Vertex>();
        verticesToVisit.Push(arbitraryStartVertex);

        while (verticesToVisit.Count > 0)
        {
            var vertex = verticesToVisit.Pop();

            foreach (var neighbor in vertex.Neighbors)
            {
                bool neighborWasDiscoveredForTheFirstTime = discoveredVertexIDs.Add(neighbor.ID);
                if (neighborWasDiscoveredForTheFirstTime)
                {
                    verticesToVisit.Push(neighbor);
                }
            }
        }

        return discoveredVertexIDs.Count == VertexCount;
    }

    public sealed class Vertex : IEquatable<Vertex>
    {
        private readonly SimpleGraph _graph;
        private readonly HashSet<Vertex> _neighbors = new HashSet<Vertex>();

        internal Vertex(SimpleGraph graph, int ID)
        {
            _graph = graph;
            this.ID = ID;
        }

        public int ID { get; }

        public IReadOnlyCollection<Vertex> Neighbors => _neighbors;
        public int Degree => _neighbors.Count;

        internal void AddNeighbor(int neighborID)
            => _neighbors.Add(_graph.Vertices[neighborID]);

        internal void AddNeighbor(Vertex neighbor)
            => _neighbors.Add(neighbor);

        public bool HasNeighbor(int neighborID)
            => _neighbors.Contains(_graph.Vertices[neighborID]);

        public bool HasNeighbor(Vertex neighbor)
            => _neighbors.Contains(neighbor);

        public override bool Equals(object obj)
            => (obj as Vertex)?.ID == ID;

        public bool Equals(Vertex other)
            => other.ID == ID;

        public override int GetHashCode()
            => ID;
    }
}

public static class Program
{
    private static void Main()
    {
        int[] firstLine = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int vertexCount = firstLine[0];
        int edgeCount = firstLine[1];

        int[,] edges = new int[edgeCount, 2];
        for (int i = 0; i < edgeCount; ++i)
        {
            int[] edge = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            edges[i, 0] = edge[0];
            edges[i, 1] = edge[1];
        }

        Console.WriteLine(
            PT07Y.Solve(vertexCount, edgeCount, edges) ? "YES" : "NO");
    }
}
