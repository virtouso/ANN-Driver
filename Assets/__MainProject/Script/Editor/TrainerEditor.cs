using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Noedify;
using System;
using System.IO;

public class TrainerEditor : EditorWindow
{

    [MenuItem("AI/ ANN")]
    public static void TrainNetwork()
    {
        TrainerEditor editorWindow = (TrainerEditor)EditorWindow.GetWindow(typeof(TrainerEditor));
       
    }


    private string _rawInputDirectory;
    private string _neuralNetName="driver";
    private AnnWrapper _annWrapper;
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Row Data Directory");
        _rawInputDirectory = GUILayout.TextField(_rawInputDirectory);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("train data")) { generateNet(); }
        if (GUILayout.Button("evaluate data")) { evaluateNet(); }
        GUILayout.BeginHorizontal();
        GUILayout.Label("network name");
        _neuralNetName = GUILayout.TextField(_neuralNetName);
        GUILayout.EndHorizontal();


    }



    private void generateNet()
    {
        _annWrapper = new AnnWrapper();

        List<float[,,]> trainingInputs = new List<float[,,]>();
        List<float[]> trainingOutputs = new List<float[]>(); ;
        Net net = _annWrapper.CompileNet(ref trainingInputs, ref trainingOutputs);
        trainNet(trainingInputs, trainingOutputs, net);
        net.SaveModel(_neuralNetName);
    }

    private  void trainNet(List<float[,,]> trainingInputs, List<float[]> trainingOutputs, Net net)
    {
        Noedify_Solver solver = Noedify.CreateSolver();
        solver.TrainNetwork(
        net,
        trainingInputs,
        trainingOutputs,
        10, // Number of epochs (iterations) to train for
        2, // Size of the training set ensemble
        0.03f, // Speed at which the solver will apply gradients
        Noedify_Solver.CostFunction.MeanSquare, // Training cost function
        Noedify_Solver.SolverMethod.MainThread, // solver computation method
        null, // (optional) weights given to training sets. 1 weight per set
        8); // Number of threads to use when using background training

        UnityEngine.Debug.Log("training process finished");
    }



    private void evaluateNet()
    {
        List<float[,,]> trainingInputs = new List<float[,,]>();
        List<float[]> trainingOutputs = new List<float[]>(); ;
        Net net =_annWrapper.CompileNet(ref trainingInputs, ref trainingOutputs);

        Noedify_Solver solver = Noedify.CreateSolver();

        
        //todo fix it in the future
        //solver.Evaluate(net,Noedify_Solver.SolverMethod.MainThread); 

    }



}
