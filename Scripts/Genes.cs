using Godot;
using System;
using System.Collections.Generic;

/*
* Biomorph genetic code
*/
public partial class Genes : Node
{

	// private int[] genes;
	private RandomNumberGenerator rng;

	// private int GENES_LENGTH = 16;

	private Dictionary<Genome.GENE_ATTRIBUTES, int> gene;


	public Genes()
	{
		// genes = new int[GENES_LENGTH];
		rng = new RandomNumberGenerator();

		// Initialize gene dictionary
		this.gene = new Dictionary<Genome.GENE_ATTRIBUTES, int>();

		// this.createNewGenes();
	}


	private void createRandomGenes()
	{

		// for (int i = 0; i < genes.Length; i++)
		// {
		// 	genes[i] = rng.RandiRange(1, 10);
		// }

		// // Color genes
		// genes[2] = rng.RandiRange(0, 255);
		// genes[3] = rng.RandiRange(0, 255);
		// genes[4] = rng.RandiRange(0, 255);
		// genes[5] = rng.RandiRange(5, 20); // Color variation range
		
		// // Structure genes
		// // Angle range
		// int a, b;
		// a = rng.RandiRange(0, 360);
		// b = rng.RandiRange(1, 359);

		// int lowerBound = Math.Min(a, b);
		// int upperBound = Math.Max(a, b);

		// genes[6] = lowerBound; 
		// genes[7] = upperBound;

		// // Radius range
		// a = rng.RandiRange(10, 100);
		// b = rng.RandiRange(11, 99);

		// lowerBound = Math.Min(a, b);
		// upperBound = Math.Max(a, b);

		// genes[8] = lowerBound; 
		// genes[9] = upperBound;

		// // Nodes range
		// a = rng.RandiRange(1, 5);
		// b = rng.RandiRange(1, 5);

		// lowerBound = Math.Min(a, b);
		// upperBound = Math.Max(a, b);

		// genes[10] = lowerBound; 
		// genes[11] = upperBound;

		// // Polygon sides range
		// a = rng.RandiRange(3, 16);
		// b = rng.RandiRange(3, 16);

		// lowerBound = Math.Min(a, b);
		// upperBound = Math.Max(a, b);

		// genes[12] = lowerBound; 
		// genes[13] = upperBound;				

	}

	override public String ToString()
	{
		String result = "";

		// foreach (int i in genes)
		// {
		// 	result = result + "[" + i + "]";
		// }

		return result;
	}

	// Returns a copy of the genetic code.
	// public int[] getGenes()
	// {
	// 	int[] copy = new int[genes.Length];
	// 	Array.Copy(genes, copy, genes.Length);
	// 	return copy;
	// }

	// Sets the genetic code of the genes instance to a new genetic code composed of the 
	// passed values.
	// public void setGenes(int depth,
	// 					 int scale,
	// 					 int r,
	// 					 int g,
	// 					 int b,
	// 					 int colorVar,
	// 					 int angleMin,
	// 					 int angleMax,
	// 					 int radMin,
	// 					 int radMax,
	// 					 int nodeMin,
	// 					 int nodeMax,
	// 					 int polyMin,
	// 					 int polyMax)
	// {
	// 	int[] newGenes = {depth, scale, r, g, b, colorVar, angleMin, angleMax, radMin, radMax, nodeMin, nodeMax, polyMin, polyMax};
	// 	this.genes = newGenes;
	// 	GD.Print("New genes: " + this.ToString());
	// }

	public void setGenes(Dictionary<Genome.GENE_ATTRIBUTES, int> newGenes)
	{
		this.gene = newGenes;
	}

	public void setGeneAttribute(Genome.GENE_ATTRIBUTES attr, int value)
	{
		this.gene.Add(attr, value);
	}

	public int getGeneAttribute(Genome.GENE_ATTRIBUTES attr)
	{
		return this.gene[attr];
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
