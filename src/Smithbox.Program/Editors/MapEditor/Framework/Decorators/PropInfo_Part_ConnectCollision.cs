﻿using Hexa.NET.ImGui;
using StudioCore.Interface;
using StudioCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioCore.Editors.MapEditor.Framework.Decorators;

public static class PropInfo_Part_ConnectCollision
{
    public static void Display(MapEditorScreen editor, Entity firstEnt)
    {
        if (firstEnt.IsPartConnectCollision())
        {
            byte[] mapIds = (byte[])PropFinderUtil.FindPropertyValue("MapID", firstEnt.WrappedObject);

            string mapString = "m";

            if (mapIds != null)
            {
                for (int i = 0; i < mapIds.Length; i++)
                {
                    var num = mapIds[i];
                    var str = "";

                    if (num == 255)
                    {
                        num = 0;
                    }

                    if (num < 10)
                    {
                        str = $"0{num}";
                    }
                    else
                    {
                        str = num.ToString();
                    }

                    if (i < mapIds.Length - 1)
                    {
                        mapString = mapString + $"{str}_";
                    }
                    else
                    {
                        mapString = mapString + $"{str}";
                    }
                }
            }
            ImGui.Separator();
            ImGui.Text($"Target Map ID:");
            ImGui.Separator();

            ImGui.Text(mapString);
            UIHelper.DisplayAlias(AliasUtils.GetMapNameAlias(editor.Project, mapString));
            ImGui.Text("");
        }
    }
}
