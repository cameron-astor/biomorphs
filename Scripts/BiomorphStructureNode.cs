using Godot;
using System.Collections.Generic;

public partial class BiomorphStructureNode : Node2D
{

	// private Polygon2D polygon;
	// private List<BiomorphStructureEdge> edges;

	// Set up structure node data.
	public void Init()
	{
		// polygon = new Polygon2D();
		
		// Vector2[] vertices = new Vector2[]
		// {
		// 	new Vector2(0, 0),
		// 	new Vector2(50, 50),
		// 	new Vector2(100, 0)
		// };

		// polygon.Polygon = vertices;
		// // polygon.Offset = new Vector2(-50, -50);

		// this.AddChild(polygon);
	}

	// Creates a polygon with equal sides and its origin at the node coordinates
	public void createPolygon(int radius, int numVertices, Color color)
	{
		Polygon2D polygon = new Polygon2D();

		List<Vector2> vertices = new List<Vector2>();

		float degreesPerVertex = 360/numVertices;
		float currentAngle = 0;
		for (int i = 0; i < numVertices; i++)
		{
			float x, y;

			float radians = Mathf.DegToRad(currentAngle);
			x = radius * Mathf.Cos(radians);
			y = radius * Mathf.Sin(radians);

			vertices.Add(new Vector2(x, y));

			currentAngle += degreesPerVertex;
		}

		Vector2[] verticesArray = vertices.ToArray();
		polygon.Polygon = verticesArray;

		// Set color
		polygon.Color = color;

		// AA
		// polygon.Antialiased = false;

		this.AddChild(polygon);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// this.Init();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

// What you should be doing here is adding the constituent shapes as children of this node.

}
