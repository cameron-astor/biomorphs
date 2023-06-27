using Godot;
using System.Collections.Generic;
using System;

public partial class Biomorph : Node2D
{

	private PackedScene biomorphStructureScene;
	private PackedScene genesScene;

	// Biomorph components
	private Genes genes;
	private BiomorphStructure structure;


	// Biomorph generation parameters
	public int DEPTH;
	public float SCALE;
	public Vector2I ANGLE_RANGE;
	public Vector2I RADIUS_RANGE;
	public Vector2I NODES_RANGE;

	public Vector2I POLYGON_SIDES_RANGE;
	public Vector2I POLYGON_RADIUS_RANGE;
	public int COLOR_VARIATION_RANGE;

	public int R, G, B;
	public int SYMMETRY_FACTOR;

	// Constructor
	public Biomorph()
	{
		// Load packed scenes
		biomorphStructureScene = GD.Load<PackedScene>("res://Scenes/BiomorphStructure.tscn");
		genesScene = GD.Load<PackedScene>("res://Scenes/Genes.tscn");
	}


	private void setupGenes(Genes g = null)
	{

		if (g == null) // If no genes are provided, generate them
		{
			var instance = genesScene.Instantiate();
			this.AddChild(instance);
			this.genes = (Genes) GetNode<Node>("Genes");
		} else {
			this.genes = g; // Use provided genes if available
		}

		int[] geneticCode = this.genes.getGenes();

		// Read genes into Biomorph generation parameters
		DEPTH = geneticCode[0];
		SCALE = geneticCode[1];
		R = geneticCode[2];
		G = geneticCode[3];
		B = geneticCode[4];
		COLOR_VARIATION_RANGE = geneticCode[5];
		ANGLE_RANGE = new Vector2I(geneticCode[6], geneticCode[7]);
		RADIUS_RANGE = new Vector2I(geneticCode[8], geneticCode[9]);
		NODES_RANGE = new Vector2I(geneticCode[10], geneticCode[11]);
		POLYGON_SIDES_RANGE = new Vector2I(geneticCode[12], geneticCode[13]);

	}

	private void createBiomorph()
	{
		int[] geneticCode = genes.getGenes();
		RandomNumberGenerator rng = new RandomNumberGenerator();

		// Setup structure
		var instance = biomorphStructureScene.Instantiate();
		this.AddChild(instance);

		structure = (BiomorphStructure) GetNode<Node2D>("BiomorphStructure");

		BiomorphStructureNode origin = structure.createNode(this.Position); // Create node at biomorph location
		
		List<BiomorphStructureNode> nodeStack = new List<BiomorphStructureNode>();
		nodeStack.Add(origin);
		for (int i = 0; i < DEPTH; i++)
		{
			List<BiomorphStructureNode> newNodeStack = new List<BiomorphStructureNode>();
			foreach (BiomorphStructureNode n in nodeStack) // For each node at the current level
			{
				int numNodesToCreate = rng.RandiRange(NODES_RANGE.X, NODES_RANGE.Y);
				for (int j = 0; j < numNodesToCreate; j++) // Create new nodes for next level
				{
					// Create new node
					int angle = rng.RandiRange(ANGLE_RANGE.X, ANGLE_RANGE.Y); // Angle of node from origin of current node
					int radius = rng.RandiRange(RADIUS_RANGE.X, RADIUS_RANGE.Y); // Distance of new node from origin of current node
					
					BiomorphStructureNode newNode = structure.createNodeAtAngle(radius, angle, n.Position);

					// Node attributes
					int polygonSides = rng.RandiRange(POLYGON_SIDES_RANGE.X, POLYGON_SIDES_RANGE.Y);
					int polygonRadius = rng.RandiRange(RADIUS_RANGE.X, RADIUS_RANGE.Y);

					// Color
					int r, g, b;
					r = R + rng.RandiRange(COLOR_VARIATION_RANGE * -1, COLOR_VARIATION_RANGE); // Slight random variation in gene expression
					g = G + rng.RandiRange(COLOR_VARIATION_RANGE * -1, COLOR_VARIATION_RANGE);
					b = B + rng.RandiRange(COLOR_VARIATION_RANGE * -1, COLOR_VARIATION_RANGE);

					Color polygonColor = new Color((float)r/255, (float)g/255, (float)b/255, 1);
					newNode.createPolygon(polygonRadius, polygonSides, polygonColor);

					newNodeStack.Add(newNode); // Keep track of new node for next level
				}

			}

			nodeStack = newNodeStack; // Swap node lists
		}

	}

	public void regenerate()
	{
		// delete all children
		foreach (Node n in this.GetChildren())
		{
			this.RemoveChild(n);
			n.QueueFree();
		}

		this.setupGenes();

		this.createBiomorph();
	}

	// Generate Biomorph from given Genes.
	public void generate(Genes g)
	{
		this.setupGenes(g);
		this.createBiomorph();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override String ToString()
	{
		String result = "";

		result += "DEPTH: " + DEPTH + "\n";
		result += "SCALE: " + SCALE + "\n";
		result += "RGB: " + R + " " + G + " " + B + " " + "\n";
		result += "COLOR VARIATION RANGE: " + COLOR_VARIATION_RANGE + "\n";
		result += "ANGLE RANGE: " + ANGLE_RANGE.ToString() + "\n";
		result += "RADIUS RANGE: " + RADIUS_RANGE.ToString() + "\n";
		result += "NODE RANGE: " + NODES_RANGE.ToString() + "\n";
		result += "POLYGON RANGE: " + POLYGON_SIDES_RANGE.ToString() + "\n";

		return result;
	}
}
