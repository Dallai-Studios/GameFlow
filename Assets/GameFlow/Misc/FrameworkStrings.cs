using System.Collections.Generic;

namespace GameFlow.Misc
{
    public static class FrameworkStrings
    {
        // Default messages
        public const string CameraReferenceTooltip = 
            "In order for this component to work properly, you must provide a camera reference to simulate the player vision";

        public const string MovementInputTooltip = 
            "Use this field to define the list of inputs to be used if the new input system is active." 
            + " If you don't want to use the new input system, you must configure every input on project settings.";

        public const string AimYInputTooltip =
            "Use this field to define the list of Aim Y inputs if the new input system is active."
            + " If you don't want to use the new input system, you must configure every input on project settings.";

        public const string AimXInputTooltip = 
            "Use this field to define the list of Aim X inputs if the new input system is active."
            + " If you don't want to use the new input system, you must configure every input on project settings.";
        
        public const string NoCameraProvided = 
            "No camera provided for the player controller. In order for this component to work properly,"
            + " you must provide a aim camera to represent the player vision.";
        
        public const string NoPlayerAttributesProvided = 
            "No player attributes provided. In order for this component to work, you must provide a Player Attributes Scriptable Object";
        
        // FPS messages
        public const string FPSConfigTooltip = 
            "In order to use this configurations, first you need to instanciate a Fps Configuration scriptable object."
            + "Go to Create > GameFlow > 3D > FPS > Fps Configurations and use this object.";
        
        public const string FPSAttributesTooltip = 
            "In order to use this attributes, first you need to instanciate a Player Controller Base Attributes scriptable object"
            + " or any other Player Controller Attributes configuration object. "
            + "Go to Create > GameFlow > Player > Attributes and choose one of the attributes configuration objects.";
        
        public const string NoFPSConfigProvided = 
            "No FPS Configurations provided. In order for this component to work, you must provide a FPS Configuration Scriptable Object";
    }
}