Shader "2 Texture Blend" {
	Properties{
		_Blend("Blend", Range(0, 1)) = 0.5
		_MainTex("Base (RGB)", 2D) = "white"
		_BlendTex("Blend (RGB)", 2D) = "black"
	}
		SubShader{
		Pass{
		SetTexture[_MainTex]

		SetTexture[_BlendTex]{
		constantColor(0, 0, 0,[_Blend])
		combine texture lerp(constant) previous
	}
	}
	}
}