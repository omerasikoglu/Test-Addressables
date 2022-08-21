using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnObjectAddressables : MonoBehaviour {

    [SerializeField] private AssetReference pfWorldAssetReference;
    [SerializeField] private AssetReference pfWorldAssetReferenceGameObject;
    [SerializeField] private AssetLabelReference pfWorldAssetLabelReference;

    private const string WORLD_KEY = "Assets/Game/Prefabs/pfWorld.prefab";
    //private const string SYSTEMS_KEY = "Assets/Game/Prefabs/pfSystems.prefab";
    private const string REMOTE_BUILD_PATH = "ServerData";
    private const string REMOTE_LOAD_PATH = "http://localhost";

    private GameObject instantiatedGO;

    private List<AsyncOperationHandle<GameObject>> asyncOperationHandleList;

    void Start() {
        //Method1();
        //Method2();
        //Method3();
        Method4();
        //Method5();
    }

    private void Method5() {

    }

    private void Method4() {
        pfWorldAssetReferenceGameObject.InstantiateAsync(transform).Completed += (asyncOperation) =>
        {
            instantiatedGO = asyncOperation.Result;
        };
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            pfWorldAssetReferenceGameObject.ReleaseInstance(instantiatedGO);
        }
    }
    private void Method3() {
        pfWorldAssetReferenceGameObject.InstantiateAsync(transform);
    }

    private void Method2() {
        Addressables.LoadAssetAsync<GameObject>(pfWorldAssetLabelReference).Completed += (asyncOperationHandle) =>
        //pfWorldAssetReference.LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) =>
        //Addressables.LoadAssetAsync<GameObject>(WORLD_KEY).Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
                Instantiate(asyncOperationHandle.Result);
            }
            else {

            }
        };
    }

    private void Method1() {


        asyncOperationHandleList = new List<AsyncOperationHandle<GameObject>>
        {
            Addressables.LoadAssetAsync<GameObject>(WORLD_KEY), 
            //Addressables.LoadAssetAsync<GameObject>(SYSTEMS_KEY),
        };

        foreach (var asyncOperationHandle in asyncOperationHandleList) {
            asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
        }
    }

    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle) {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
            Instantiate(asyncOperationHandle.Result);
        }
        else {

        }
    }
}
