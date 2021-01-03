using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Noedify;

public class AnnWrapper
{
    public Net CompileNet(ref List<float[,,]> trainingInputs, ref List<float[]> trainingOutputs)
    {

        Noedify.Net net = new Net();
        _readRawImages(ref trainingInputs, ref trainingOutputs);
        Noedify.Layer inputLayer = new Noedify.Layer(Noedify.LayerType.Input, 1920, "input layer");
        net.AddLayer(inputLayer);
        Noedify.Layer hiddenLayer0 = new Noedify.Layer(Noedify.LayerType.FullyConnected, 1200, "fully connected 1");
        net.AddLayer(hiddenLayer0);
        Noedify.Layer outputLayer = new Noedify.Layer(Noedify.LayerType.Output, 3, Noedify.ActivationFunction.Tanh, "output layer");
        net.AddLayer(outputLayer);
        net.BuildNetwork();
        return net;
    }





    public void _readRawImages(ref List<float[,,]> trainingInputs, ref List<float[]> trainingOutputs)
    {

        trainingInputs = new List<float[,,]>();
        string directory = Application.dataPath + "/screenshots/";
        foreach (string file in Directory.EnumerateFiles(directory, "*.png"))
        {

            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(file))
            {
                fileData = File.ReadAllBytes(file);
                tex = new Texture2D(32, 20, TextureFormat.R8, false);
                tex.LoadImage(fileData);
                Color[] colorArray = tex.GetPixels();
                // texList[0] = tex;
                // textureList.Add(texList);
                float[,,] convertedFloat = generateFloatArray(colorArray);



                trainingInputs.Add(convertedFloat);
                string fileName = Path.GetFileName(file).Replace(".png", "");
                string[] output = fileName.Split('_');
                float[] floatOutputs = new float[output.Length - 1];
                for (int i = 0; i < floatOutputs.Length; i++)
                {
                    floatOutputs[i] = float.Parse(output[i + 1]);
                }
                trainingOutputs.Add(floatOutputs);
            }

        }


        //  Noedify_Utils.ImportImageData(ref trainingInput, ref trainingOutputs, textureList, false);

    }

    public float[,,] generateFloatArray(Color[] colorArray)
    {
        float[,,] finalrResult = new float[colorArray.Length, 3, 1];

        for (int i = 0; i < colorArray.Length; i++)
        {
            finalrResult[i, 0, 0] = colorArray[i].r;
            finalrResult[i, 1, 0] = colorArray[i].g;
            finalrResult[i, 2, 0] = colorArray[i].b;

        }

        return finalrResult;
    }

    public void Texture2dToFloatArray(ref float[,,] output, Texture2D txt)
    {
        Noedify_Utils.ImportImageData(ref output, txt, false);
    }
















}
