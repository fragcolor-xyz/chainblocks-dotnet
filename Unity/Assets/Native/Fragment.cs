using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Chainblocks;

public interface IFragmentProvider
{
  string GetCode();
}

public class Fragment : MonoBehaviour
{
  public IFragmentProvider FragmentProvider;

  public string URI;

  void Awake()
  {

  }

  void Update()
  {
  }
}