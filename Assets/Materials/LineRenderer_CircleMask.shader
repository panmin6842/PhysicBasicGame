Shader "Custom/LineRenderer_CircleMask"
{
    Properties //������� �������̽��� �ٷ�
    {
        _MaskCenter ("Mask Center (World)", Vector) = (0, 0, 0, 0)
        _MaskRadius ("Mask Radius", Float) = 5.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" } //������ ������Ʈ�� ���߿� �׸��� ��
        Blend SrcAlpha OneMinusSrcAlpha //�ҽ��� ����� ��� ���� ���ΰ� ����ȭ
        ZWrite Off //Z-Buffer ��� �� �� �� ī�޶� �������� ������Ʈ�� �������ִ� �� ��� �� ��
        Cull Off //�� �� ��� ������
        LOD 100 //���� ������ ���̴�

        Pass
        {
            CGPROGRAM
            #pragma vertex vert //fragment shader�� ���ø� �ϱ� ���� �ʿ�, ����
            #pragma fragment frag //�ȼ��� �÷��� ����ϰ� ����ϱ� ����

            float4 _MaskCenter;
            float _MaskRadius;

            //�Է� ����(���ؽ�)
            struct appdata
            {
                float4 vertex : POSITION; //���ؽ� ������
                fixed4 color : COLOR; //���ؽ��� �÷�
            };

            //�ȼ� ���̴��� �ѱ� ����
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 worldPos : TEXCOORD0; //ù�� ° UV��ǥ, ���� ��ġ���� ������ �Ÿ� ��꿡 �ʿ�
                fixed4 color : COLOR; //LineRenderer���� ���� ����
            };

            v2f vert (appdata v) //�� ���ؽ��� ����Ǵ� ���α׷�, fragment shader���� �ؽ�ó�� ���ø� �ϱ� ���� �ʿ�, ���� ��ǥ�� ������ �Ѱ���
            {
                v2f o;
                float4 worldPos = mul(unity_ObjectToWorld, v.vertex); //������ Ŭ�� �������� ��ȯ
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = worldPos.xy;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target //���� ���е� RGBA �÷��� ��ȯ, �� �ȼ����� ���Ǵ� �Լ� 
            {
                float dist = distance(i.worldPos, _MaskCenter.xy);
                if (dist > _MaskRadius) //����ũ ���̸� discard�� �׸��� ����
                    discard;

                return i.color;
            }
            ENDCG
        }
    }
}