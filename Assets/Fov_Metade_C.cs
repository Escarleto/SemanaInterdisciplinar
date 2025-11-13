using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class Fov_Metade_C : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material blurMaterial; // 🔸 material com o shader que faz o desfoque parcial
    }

    public Settings settings = new Settings();

    class CustomRenderPass : ScriptableRenderPass
    {
        private Material blurMat;

        public CustomRenderPass(Material material)
        {
            this.blurMat = material;
        }

        // PassData é usado pelo RenderGraph
        private class PassData
        {
            public Material mat;
        }

        static void ExecutePass(PassData data, RasterGraphContext context)
        {
            // Faz o DrawFullScreen com o material (blur)
            CoreUtils.DrawFullScreen(context.cmd, data.mat);
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (blurMat == null)
                return;

            const string passName = "Metade Blur Pass";

            using (var builder = renderGraph.AddRasterRenderPass<PassData>(passName, out var passData))
            {
                passData.mat = blurMat;

                UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
                builder.SetRenderAttachment(resourceData.activeColorTexture, 0);
                builder.SetRenderFunc((PassData data, RasterGraphContext context) =>
                {
                    ExecutePass(data, context);
                });
            }
        }
    }

    CustomRenderPass m_ScriptablePass;

    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass(settings.blurMaterial);
        m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.blurMaterial != null)
            renderer.EnqueuePass(m_ScriptablePass);
    }
}
