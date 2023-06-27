using Godot;
using System;

/*
* User interface for editing a gene.
*/
public partial class GeneEditor : Control
{

	// Signals
	[Signal]
	public delegate void SentGenesEventHandler(Genes g);

	// UI elements
	Control depth, scale, color, colorVariation, angleRange, radiusRange, nodesRange, polygonRange;
	Label depthLabel, scaleLabel, colorVariationLabel, angleRangeLabel, radiusRangeLabel, nodesRangeLabel, polygonRangeLabel;
	Slider depthSlider, scaleSlider, colorVariationSlider, angleRangeMin, angleRangeMax, radiusRangeMin, radiusRangeMax,
		   nodesRangeMin, nodesRangeMax, polygonRangeMin, polygonRangeMax;
	ColorPickerButton colorPicker;	
	Button generate, randomize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get references to UI elements

		// Depth
		depth = (Control) this.GetNode("Panel/Depth");
		depthLabel = (Label) depth.GetNode("Label");
		depthSlider = (Slider) depth.GetNode("HSlider");

		// Scale
		scale = (Control) this.GetNode("Panel/Scale");
		scaleLabel = (Label) scale.GetNode("Label");
		scaleSlider = (Slider) scale.GetNode("HSlider");

		// Color
		color = (Control) this.GetNode("Panel/Color");
		colorPicker = (ColorPickerButton) color.GetNode("ColorPickerButton");

		// Color variation
		colorVariation = (Control) this.GetNode("Panel/Color Variation");
		colorVariationLabel = (Label) colorVariation.GetNode("Label");
		colorVariationSlider = (Slider) colorVariation.GetNode("HSlider");

		// Angle range
		angleRange = (Control) this.GetNode("Panel/Angle Range");
		angleRangeLabel = (Label) angleRange.GetNode("Label");
		angleRangeMin = (Slider) angleRange.GetNode("Min"); 
		angleRangeMax = (Slider) angleRange.GetNode("Max");

		// Radius range
		radiusRange = (Control) this.GetNode("Panel/Radius Range");
		radiusRangeLabel = (Label) radiusRange.GetNode("Label");
		radiusRangeMin = (Slider) radiusRange.GetNode("Min"); 
		radiusRangeMax = (Slider) radiusRange.GetNode("Max");

		// Nodes range
		nodesRange = (Control) this.GetNode("Panel/Nodes Range");
		nodesRangeLabel = (Label) nodesRange.GetNode("Label");
		nodesRangeMin = (Slider) nodesRange.GetNode("Min"); 
		nodesRangeMax = (Slider) nodesRange.GetNode("Max");

		// Polygon range
		polygonRange = (Control) this.GetNode("Panel/Polygon Sides Range");
		polygonRangeLabel = (Label) polygonRange.GetNode("Label");
		polygonRangeMin = (Slider) polygonRange.GetNode("Min"); 
		polygonRangeMax = (Slider) polygonRange.GetNode("Max");


		// Setup signals
		Button gen_button = (Button) this.GetNode("Panel/Generate");
		gen_button.Pressed += OnGeneratePressed;

	}

	private void OnGeneratePressed()
	{
		Genes g = createGenes();
		EmitSignal(SignalName.SentGenes, g);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		depthLabel.Text = "Depth: " + depthSlider.Value;
		scaleLabel.Text = "Scale: " + scaleSlider.Value;
		colorVariationLabel.Text = "Color Variation: " + colorVariationSlider.Value;

		// Angle
		angleRangeLabel.Text = "Angle: " + angleRangeMin.Value + " - " + angleRangeMax.Value; 
		// Control angle sliders
		angleRangeMin.MaxValue = angleRangeMax.Value;
		angleRangeMax.MinValue = angleRangeMin.Value;

		// Radius
		radiusRangeLabel.Text = "Radius: " + radiusRangeMin.Value + " - " + radiusRangeMax.Value; 
		// Control radius sliders
		radiusRangeMin.MaxValue = radiusRangeMax.Value;
		radiusRangeMax.MinValue = radiusRangeMin.Value;

		// Nodes
		nodesRangeLabel.Text = "Nodes: " + nodesRangeMin.Value + " - " + nodesRangeMax.Value; 
		// Control nodes sliders
		nodesRangeMin.MaxValue = nodesRangeMax.Value;
		nodesRangeMax.MinValue = nodesRangeMin.Value;

		// Polygon sides
		polygonRangeLabel.Text = "Polygon Sides: " + polygonRangeMin.Value + " - " + polygonRangeMax.Value; 
		// Control polygon sliders
		polygonRangeMin.MaxValue = polygonRangeMax.Value;
		polygonRangeMax.MinValue = polygonRangeMin.Value;
	}

	private Genes createGenes()
	{
		Genes g = new Genes();

		g.setGenes((int) depthSlider.Value,
				   (int) scaleSlider.Value,
				   (int) (colorPicker.Color.R * 255),
				   (int) (colorPicker.Color.G * 255),
				   (int) (colorPicker.Color.B * 255),
				   (int) colorVariationSlider.Value,
				   (int) angleRangeMin.Value,
				   (int) angleRangeMax.Value,
				   (int) radiusRangeMin.Value,
				   (int) radiusRangeMax.Value,
				   (int) nodesRangeMin.Value,
				   (int) nodesRangeMax.Value,
				   (int) polygonRangeMin.Value,
				   (int) polygonRangeMax.Value);

		return g;
	}

}
