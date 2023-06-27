using Godot;
using System.Collections.Generic;
public partial class BiomorphStructure : Node2D
{

	private PackedScene structureNodeScene;
	private PackedScene structureEdgeScene;
	
	private List<BiomorphStructureEdge> edges;
	private List<BiomorphStructureNode> nodes;

	private Vector2 origin; // The origin coordinates of the biomorph structure

	// Constructor
	public BiomorphStructure()
	{
		// Load packed scenes
		structureNodeScene = GD.Load<PackedScene>("res://Scenes/biomorph_structure_node.tscn");
		structureEdgeScene = GD.Load<PackedScene>("res://Scenes/biomorph_structure_edge.tscn");

		// Initialize containers
		edges = new List<BiomorphStructureEdge>();
		nodes = new List<BiomorphStructureNode>();

	}

	// Takes angle in degrees
	public BiomorphStructureNode createNodeAtAngle(int radius, int angle, Vector2 origin)
	{
		BiomorphStructureNode newNode = (BiomorphStructureNode)structureNodeScene.Instantiate();

		float x, y;

		// Position calculation from radius and angle
		float radians = Mathf.DegToRad(angle);

		x = radius * Mathf.Cos(radians);
		y = radius * Mathf.Sin(radians);

		newNode.Position = new Vector2(x, y) + origin; // Add offset

		nodes.Add(newNode);
		this.AddChild(newNode);

		return newNode;
	}

	// Create a new node of the biomorph structure and add it to the scene tree.
	// Returns a reference to the node.
	public BiomorphStructureNode createNode(Vector2 pos)
	{
		BiomorphStructureNode newNode = (BiomorphStructureNode) structureNodeScene.Instantiate();
		
		newNode.Position = pos;

		nodes.Add(newNode);
		this.AddChild(newNode);

		return newNode;
	}

	// Creates a new edge given a starting and ending node and adds it to the tree.
	// Returns a reference to the edge.
	public BiomorphStructureEdge createEdge(BiomorphStructureNode startNode, BiomorphStructureNode endNode)
	{
		BiomorphStructureEdge newEdge = (BiomorphStructureEdge)structureEdgeScene.Instantiate();

		newEdge.Init(startNode, endNode);

		edges.Add(newEdge);
		this.AddChild(newEdge);

		return newEdge;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}
