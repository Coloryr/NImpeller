using System.Diagnostics;
using System.Numerics;
using NImpeller;

namespace Sandbox.Scenes;

public class ParagraphScene : IScene
{
    Stopwatch st = Stopwatch.StartNew();
    public void Render(ImpellerContext context, ImpellerDisplayListBuilder scene, SceneParameters sceneParameters)
    {
        using var paint = ImpellerPaint.New()!;
        paint.SetColor(ImpellerColor.FromRgb(255, 0, 0));
                
        var paragraphText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        var fontSize = 24.0f;
        var maxWidth = sceneParameters.Width - 100.0f;

        using var typographyContext = ImpellerTypographyContext.New();
        using var paragraphStyle = ImpellerParagraphStyle.New()!;
        paragraphStyle.SetFontSize(fontSize);
        paragraphStyle.SetTextAlignment(ImpellerTextAlignment.kImpellerTextAlignmentLeft);
        paragraphStyle.SetMaxLines(3);
        paragraphStyle.SetEllipsis("...");

        using var paragraphBuilder = typographyContext!.ParagraphBuilderNew();
        paragraphBuilder!.PushStyle(paragraphStyle);
        paragraphBuilder.AddText(paragraphText);
        using var paragraph = paragraphBuilder.BuildParagraphNew(maxWidth);

        var x = 50.0f;
        var y = (sceneParameters.Height - paragraph!.GetHeight()) / 2.0f;
        scene.DrawParagraph(paragraph, new ImpellerPoint { X = x, Y = y });
    }
}