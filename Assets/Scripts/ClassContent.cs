using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ClassContent : MonoBehaviour
{
	#region Singleton

	private static ClassContent instance;
	public static ClassContent Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new ClassContent();
			}
			return instance;
		}
	}

	#endregion

	[Serializable]
	public class Questions
	{
		public string name;
		public string question;
		public string answerTrue;
		public string answerFalse01;
		public string answerFalse02;
		public string answerFalse03;
	}

	[Header("Gameplay")]
	public int actualQuestion = 0;
	public int totalQuestions = 0;
	public int asnwerTrueId = 0;
	public int asnwerCorrect = 0;
	public int asnwerIncorrect = 0;

	[Header("Questions")]
	[Range(0f, 5f)] public float timeToSelect = 3;
	public TextMeshProUGUI txtTitle;
	[Space]
	public Image[] imgAnswers;
	[Space]
	public List<TextMeshProUGUI> txtAnswers;
	[Space]
	public Color colorEnter;
	public Color colorExit;
	public Color colorTrue;
	public Color colorFalse;

	[Header("Next Question")]
	public bool isChanging = false;
	[Range(0f, 5f)] public float timeToChange = 3;
	[Space]
	public PlayableAsset playableShowOn;
	public PlayableAsset playableNext;
	public PlayableAsset playableShowOff;

	[Header("JSON")]
	public string fileName = "Questions";
	public Questions[] questions;
	private string fileFormat = ".json";

	PlayableDirector playableDirector;
	List<string> answers;
	List<TextMeshProUGUI> texts;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		playableDirector = GetComponent<PlayableDirector>();
		answers = new List<string>();
		texts = new List<TextMeshProUGUI>();
	}

	private void Start()
	{
		StartCoroutine(LoadJsonData());
	}

	public void CheckQuestion(int id)
	{
		isChanging = true;

		// Seteo los colores de Verdadero o Falso
		for (int i = 0; i < imgAnswers.Length; i++)
			imgAnswers[i].color = i == asnwerTrueId ? colorTrue : colorFalse;

		// Agrego puntos
		if (asnwerTrueId == id)
			asnwerCorrect++;
		else
			asnwerIncorrect++;

		Invoke("NextQuestion", timeToChange);
	}

	public void NextQuestion()
	{
		playableDirector.playableAsset = playableNext;
		playableDirector.Play(); ;
	}

	public void ShowQuestion(bool isShowing)
	{
		playableDirector.playableAsset = isShowing ? playableShowOn : playableShowOff;
		playableDirector.Play(); ;
	}

	private void FinishGame()
	{
		for (int i = 0; i < imgAnswers.Length; i++)
			imgAnswers[i].GetComponent<BoxCollider>().enabled = false;

		txtTitle.text = "Results";

		imgAnswers[0].color = colorTrue;
		txtAnswers[0].text = asnwerCorrect.ToString();

		txtAnswers[1].text = "-";

		imgAnswers[2].color = colorFalse;
		txtAnswers[2].text = asnwerIncorrect.ToString();
	}

	#region JSON ------------------------------------

	IEnumerator LoadJsonData()
	{

#if UNITY_EDITOR
		string filePath = Path.Combine(Application.streamingAssetsPath, fileName + fileFormat);
		LoadData(filePath);

#elif UNITY_IOS
		string filePath = Path.Combine(Application.dataPath + "/Raw", fileName + fileFormat);
		LoadData(filePath);

#elif UNITY_ANDROID
		string filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName + fileFormat);
		string fileJson;
		if (filePath.Contains("://") || filePath.Contains(":///"))
		{
			UnityWebRequest www = UnityWebRequest.Get(filePath);
			yield return www.Send();
			fileJson = www.downloadHandler.text;
		}
		else
		{
			fileJson = File.ReadAllText(filePath);
		}

		questions = JsonHelper.FromJsonArray<Questions>(fileJson);
		totalQuestions = questions.Length - 1;

		ChangeQuestions();
		ShowQuestion(true);

#endif

		yield return null;
	}

	private void LoadData(string _filePath)
	{
		if (File.Exists(_filePath))
		{
			string fileJson = File.ReadAllText(_filePath);

			questions = Util.FromJsonArray<Questions>(fileJson);
			totalQuestions = questions.Length - 1;

			ChangeQuestions();
			ShowQuestion(true);
		}
		else
		{
			Debug.LogError("<color=red><b>" + "ERROR: " + "</b></color>" + "No se encontro el archivo JSON. ");
		}
	}

	#endregion ----------------------------------

	#region Timeline ----------------------------

	public void SetChange(bool _isChanging)
	{
		isChanging = _isChanging;
	}

	public void ChangeQuestions()
	{
		// Reseteo de color
		for (int i = 0; i < imgAnswers.Length; i++)
			imgAnswers[i].color = colorExit;

		// Termina el juego si se terminan las preguntas
		if (actualQuestion == questions.Length)
		{
			FinishGame();
			return;
		}

		// Seteamos la pregunta principal
		txtTitle.text = questions[actualQuestion].question;

		// Se agrega contenido a las listas temporales
		texts.AddRange(txtAnswers);
		answers.Add(questions[actualQuestion].answerFalse01);
		answers.Add(questions[actualQuestion].answerFalse02);
		answers.Add(questions[actualQuestion].answerFalse03);

		// Random para Pregunta Correcta
		int r = UnityEngine.Random.Range(0, texts.Count);

		asnwerTrueId = r;
		texts[r].text = questions[actualQuestion].answerTrue;
		texts.Remove(texts[r]);

		// Random para Preguntas Falsas
		for (int i = 0; i < texts.Count; i++)
		{
			r = UnityEngine.Random.Range(0, answers.Count);
			texts[i].text = answers[r];
			answers.Remove(answers[r]);
		}

		actualQuestion++;

		// Se limpian todas las listas
		answers.Clear();
		texts.Clear();
	}

	#endregion ----------------------------------
}
