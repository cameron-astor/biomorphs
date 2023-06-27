using Godot;
using System;

public partial class BiomorphStructureEdge : Line2D
{

	private BiomorphStructureNode start;
	private BiomorphStructureNode end;

	public void Init(BiomorphStructureNode s, BiomorphStructureNode e)
	{
		this.AddPoint(s.Position);
		this.AddPoint(e.Position);
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
