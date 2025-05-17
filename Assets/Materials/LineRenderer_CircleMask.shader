Shader "Custom/LineRenderer_CircleMask"
{
    Properties //사용자의 인터페이스를 다룸
    {
        _MaskCenter ("Mask Center (World)", Vector) = (0, 0, 0, 0)
        _MaskRadius ("Mask Radius", Float) = 5.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" } //반투명 오브젝트를 나중에 그리게 함
        Blend SrcAlpha OneMinusSrcAlpha //소스와 배경을 어떻게 섞을 것인가 투명화
        ZWrite Off //Z-Buffer 사용 안 함 즉 카메라를 기준으로 오브젝트가 떨어져있는 값 사용 안 함
        Cull Off //앞 뒤 모두 렌더링
        LOD 100 //가장 간단한 쉐이더

        Pass
        {
            CGPROGRAM
            #pragma vertex vert //fragment shader를 샘플링 하기 위해 필요, 정점
            #pragma fragment frag //픽셀의 컬러를 계산하고 출력하기 위해

            float4 _MaskCenter;
            float _MaskRadius;

            //입력 구조(버텍스)
            struct appdata
            {
                float4 vertex : POSITION; //버텍스 포지션
                fixed4 color : COLOR; //버텍스당 컬러
            };

            //픽셸 쉐이더에 넘길 구조
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 worldPos : TEXCOORD0; //첫번 째 UV좌표, 월드 위치에서 원과의 거리 계산에 필요
                fixed4 color : COLOR; //LineRenderer에서 받은 색상
            };

            v2f vert (appdata v) //각 버텍스에 실행되는 프로그램, fragment shader에서 텍스처를 샘플링 하기 위해 필요, 월드 좌표와 색상을 넘겨줌
            {
                v2f o;
                float4 worldPos = mul(unity_ObjectToWorld, v.vertex); //정점을 클립 공간으로 변환
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = worldPos.xy;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target //낮은 정밀도 RGBA 컬러를 반환, 각 픽셀마다 계산되는 함수 
            {
                float dist = distance(i.worldPos, _MaskCenter.xy);
                if (dist > _MaskRadius) //마스크 밖이면 discard로 그리기 멈춤
                    discard;

                return i.color;
            }
            ENDCG
        }
    }
}