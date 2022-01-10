using UnityEngine;

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
