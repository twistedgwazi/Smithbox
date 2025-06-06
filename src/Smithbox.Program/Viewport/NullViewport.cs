﻿using Hexa.NET.ImGui;
using StudioCore.Editors.MapEditor;
using StudioCore.Editors.ModelEditor;
using System.Numerics;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.Utilities;

namespace StudioCore.ViewportNS;

/// <summary>
///     A null viewport that doesn't actually do anything
/// </summary>
public class NullViewport : IViewport
{
    public Smithbox BaseEditor;
    public MapEditorScreen MapEditor;
    public ModelEditorScreen ModelEditor;

    public ViewportType ViewportType;

    private readonly string _vpid = "";

    public int X;
    public int Y;

    private ViewportType _viewportType;
    public NullViewport(Smithbox baseEditor, MapEditorScreen mapEditor, ModelEditorScreen modelEditor, ViewportType viewportType, string id, int width, int height)
    {
        _vpid = id;

        BaseEditor = baseEditor;
        MapEditor = mapEditor;
        ModelEditor = modelEditor;
        ViewportType = viewportType;

        Width = width;
        Height = height;

        ViewportCamera = new ViewportCamera(BaseEditor, this, viewportType, new Rectangle(0, 0, Width, Height));
    }

    public ViewportCamera ViewportCamera { get; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public float NearClip { get; set; } = 0.1f;
    public float FarClip { get; set; } = CFG.Current.Viewport_RenderDistance_Max;

    public bool IsViewportSelected { get; set; }

    public void OnGui()
    {
        if (CFG.Current.Interface_Editor_Viewport)
        {
            if (ImGui.Begin($@"Viewport##{_vpid}", ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoNav))
            {
                Vector2 p = ImGui.GetWindowPos();
                Vector2 s = ImGui.GetWindowSize();
                var newvp = new Rectangle((int)p.X, (int)p.Y + 3, (int)s.X, (int)s.Y - 3);
                ResizeViewport(null, newvp);
                ImGui.Text("Disabled...");
            }

            ImGui.End();
        }

        if (CFG.Current.Viewport_Profiling)
        {
            if (ImGui.Begin($@"Profiling##{_vpid}"))
            {
                ImGui.Text(@"Disabled...");
            }

            ImGui.End();
        }
    }

    public void SceneParamsGui()
    {
    }

    public void ResizeViewport(GraphicsDevice device, Rectangle newvp)
    {
        Width = newvp.Width;
        Height = newvp.Height;
        X = newvp.X;
        Y = newvp.Y;
        ViewportCamera.UpdateBounds(newvp);
    }

    public bool Update(Sdl2Window window, float dt)
    {
        return false;
    }

    public void Draw(GraphicsDevice device, CommandList cl)
    {
    }

    public void SetEnvMap(uint index)
    {
    }

    public void FrameBox(BoundingBox box)
    {
    }

    public void FramePosition(Vector3 pos, float dist)
    {
    }
}
