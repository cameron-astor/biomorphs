using Godot;
using System.Collections.Generic;
using System;

public partial class Biomorph : Node2D
{

	const int POLYGON_MIN_SIZE = 5;

	private PackedScene biomorphStructureScene;
	private PackedScene genesScene;

	// Biomorph components
	private Genes genes;
	private BiomorphStructure structure;
	private RandomNumberGenerator rng;


	// Constructor
	public Biomorph()
	{
		// Load packed scenes
		biomorphStructureScene = GD.Load<PackedScene>("res://Scenes/BiomorphStructure.tscn");
		genesScene = GD.Load<PackedScene>("res://Scenes/Genes.tscn");

		rng = new RandomNumberGenerator();
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

	}

	private void createBiomorph()
	{

		// Setup structure
		BiomorphStructure structure = (BiomorphStructure) biomorphStructureScene.Instantiate();
		this.AddChild(structure);

		// Parameters which apply globally to structure
		int CURRENT_LAYER_SCALE = this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.SCALE); // Will change according to SCALE_LAYER_MODIFIER


		BiomorphStructureNode origin = structure.createNode(this.Position); // Create origin node at biomorph location
		this.generateNodePolygon(origin, CURRENT_LAYER_SCALE);
		
		List<BiomorphStructureNode> nodeStack = new List<BiomorphStructureNode>();
		nodeStack.Add(origin);
		for (int i = 0; i < this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.DEPTH); i++)
		{
			List<BiomorphStructureNode> newNodeStack = new List<BiomorphStructureNode>();
			foreach (BiomorphStructureNode n in nodeStack) // For each node at the current level
			{
				int numNodesToCreate = rng.RandiRange(this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.NODES_RANGE_MIN), 
												      this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.NODES_RANGE_MAX));
				for (int j = 0; j < numNodesToCreate; j++) // Create new nodes for next level
				{
					// Create new node
					// Angle of node from origin of current node
					int angle = rng.RandiRange(this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.ANGLE_RANGE_MIN), 
											   this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.ANGLE_RANGE_MAX));

					// Distance of new node from origin of current node					    
					int radius = rng.RandiRange(this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.POLYGON_RADIUS_RANGE_MIN), 
											    this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.POLYGON_RADIUS_RANGE_MAX)); 
					
					BiomorphStructureNode newNode = structure.createNodeAtAngle(radius, angle, n.Position);

					this.generateNodePolygon(newNode, CURRENT_LAYER_SCALE);

					newNodeStack.Add(newNode); // Keep track of new node for next level
				}

				// Calculate next layer parameters
				CURRENT_LAYER_SCALE += this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.SCALE_LAYER_MODIFIER);

			}

			nodeStack = newNodeStack; // Swap node lists
		}

	}

	// Handles the generation of the polygon associated with a particular new node.
	// Takes in a reference to the node and structural attributes.
	//
	// POTENTIAL REFACTOR: The structural parameters should maybe be global to the Biomorph (no need for parameters here).
	private void generateNodePolygon(BiomorphStructureNode newNode, int CURRENT_LAYER_SCALE)
	{
		// Node attributes
		int polygonSides = rng.RandiRange(this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.POLYGON_SIDES_RANGE_MIN), 
											this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.POLYGON_SIDES_RANGE_MAX));

		// Polygon scale							  
		int scaleVariation = this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.SCALE_VARIATION);
		int polygonRadius = Math.Max(CURRENT_LAYER_SCALE + rng.RandiRange(scaleVariation * -1, scaleVariation), POLYGON_MIN_SIZE);

		// Color
		int r, g, b;
		r = this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.COLOR_R);
		g = this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.COLOR_G);
		b = this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.COLOR_B);

		int variation = this.genes.getGeneAttribute(Genome.GENE_ATTRIBUTES.COLOR_VARIATION_RANGE);

		r = r + rng.RandiRange(variation * -1, variation); // Slight random variation in gene expression
		g = g + rng.RandiRange(variation * -1, variation);
		b = b + rng.RandiRange(variation * -1, variation);

		Color polygonColor = new Color((float)r/255, (float)g/255, (float)b/255, 1);
		newNode.createPolygon(polygonRadius, polygonSides, polygonColor);
	}

	// Generate Biomorph from given Genes.
	public void generate(Genes g)
	{
		this.setupGenes(g);
		this.createBiomorph();
	}

	public override String ToString()
	{
		String result = "";

		// result += "DEPTH: " + DEPTH + "\n";
		// result += "SCALE: " + SCALE + "\n";
		// result += "RGB: " + R + " " + G + " " + B + " " + "\n";
		// result += "COLOR VARIATION RANGE: " + COLOR_VARIATION_RANGE + "\n";
		// result += "ANGLE RANGE: " + ANGLE_RANGE.ToString() + "\n";
		// result += "RADIUS RANGE: " + RADIUS_RANGE.ToString() + "\n";
		// result += "NODE RANGE: " + NODES_RANGE.ToString() + "\n";
		// result += "POLYGON RANGE: " + POLYGON_SIDES_RANGE.ToString() + "\n";

		return result;
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
