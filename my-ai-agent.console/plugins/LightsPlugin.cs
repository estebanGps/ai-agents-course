using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace MyAiAgent.Console.Plugins;

public class LightsPlugin
{
    private readonly List<Light> _lights =
    [
        new Light { Id = 1, Name = "Table Lamp", IsOn = false },
        new Light { Id = 2, Name = "Porch Light", IsOn = false },
        new Light { Id = 3, Name = "Chandelier", IsOn = true },
    ];

    [KernelFunction("get_lights")]
    [Description("Gets a list of lights and their current state")]
    public List<Light> GetLights()
    {
        return _lights;
    }

    [KernelFunction("change_state")]
    [Description("Changes the state of the light")]
    public Light? ChangeState(int id, bool isOn)
    {
        Light? light = _lights.FirstOrDefault(l => l.Id == id);
        if (light == null)
            return null;

        light.IsOn = isOn;
        return light;
    }
}