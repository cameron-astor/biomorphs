using Godot;
using System;

public partial class Main : Node
{

	private PackedScene biomorphScene;

	private Biomorph b;
	private GeneEditor ui;

	public Main()
	{
		biomorphScene = GD.Load<PackedScene>("res://Scenes/Biomorph.tscn");
	}

	private void setupBiomorphs()
	{
		Biomorph biomorph = (Biomorph) biomorphScene.Instantiate();
		b = biomorph;
		b.Position = new Vector2(DisplayServer.WindowGetSize().X/2, DisplayServer.WindowGetSize().Y/2); // Center biomorph
		this.AddChild(b);
	}

	// UI functions
	private void OnGeneratePressed(Genes g)
	{
		// GD.Print("Generate pressed. Creating new biomorph...");
		// GD.Print(g);

		b.QueueFree(); // Delete biomorph

		b = (Biomorph) biomorphScene.Instantiate(); // new biomorph
		b.Position = new Vector2(DisplayServer.WindowGetSize().X/4, DisplayServer.WindowGetSize().Y/4); // Center biomorph
		this.AddChild(b);
		b.generate(g);

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		// ---- User interface setup ----

		// Get reference to UI
		this.ui = (GeneEditor) this.GetNode("GeneEditor");

		// Connect UI signals
		// Generate button
		// Button gen_button = (Button) ui.GetNode("Panel/Generate");
		// gen_button.Pressed += OnGeneratePressed;
		this.ui.SentGenes += OnGeneratePressed;

		setupBiomorphs();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept"))
		{

		}
    }
}
