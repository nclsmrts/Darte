using UnityEngine;

public class AlvoManager : MonoBehaviour
{
    private float intervaloPosicao = 3f;
    private float tempoProximaMudanca;
    private Vector3 proximaPosicao;
    private Vector3 posicaoInicial;
    private float tempoTransicao = 1f;
    private float tempoTransicaoAtual = 0f;
    private bool emTransicao = false;

    void Start()
    {
        posicaoInicial = transform.position;
        proximaPosicao = GerarNovaPosicao();
        tempoProximaMudanca = Time.time + intervaloPosicao;
    }

    void Update()
    {
        if (!emTransicao && Time.time >= tempoProximaMudanca)
        {
            posicaoInicial = transform.position;
            proximaPosicao = GerarNovaPosicao();
            tempoTransicaoAtual = 0f;
            emTransicao = true;
            tempoProximaMudanca = Time.time + intervaloPosicao;
        }

        if (emTransicao)
        {
            tempoTransicaoAtual += Time.deltaTime;
            float t = Mathf.Clamp01(tempoTransicaoAtual / tempoTransicao);
            transform.position = Vector3.Lerp(posicaoInicial, proximaPosicao, t);

            if (t >= 1f)
            {
                emTransicao = false;
            }
        }
    }

    private Vector3 GerarNovaPosicao()
    {
        // Altere os valores conforme o espaço desejado para movimentação
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(1f, 2f);
        //float z = Random.Range(-5f, 5f);
        return new Vector3(x, y, transform.position.z);
    }
}
