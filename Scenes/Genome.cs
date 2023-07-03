using Godot;
using System;

public partial class Genome : Node
{

	public enum GENE_ATTRIBUTES {
		DEPTH, 
		SCALE, SCALE_VARIATION, SCALE_LAYER_MODIFIER,
		COLOR_R, COLOR_G, COLOR_B, COLOR_VARIATION_RANGE,
		NODES_RANGE_MIN, NODES_RANGE_MAX,
		ANGLE_RANGE_MIN, ANGLE_RANGE_MAX,
		POLYGON_RADIUS_RANGE_MIN, POLYGON_RADIUS_RANGE_MAX,
		POLYGON_SIDES_RANGE_MIN, POLYGON_SIDES_RANGE_MAX
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
