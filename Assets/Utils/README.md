LocalData.Save<ite>(ref example, "ExampleJsonLocal", Application.persistentDataPath);
        LocalData.Load<ite>("ExampleJsonLocal", Application.persistentDataPath);

        CloudData.SaveInDropbox<ite>(ref example, "ExampleJsonCloud", "/Data", new User("","",""));
        CloudData.LoadOfDropbox<ite>("ExampleJsonCloud","/Data", new User("","",""));

        CloudData.SaveZipInDropbox(Application.persistentDataPath, "ExampleZip", "/Data", new User("","",""));
        CloudData.LoadZipOfDropbox("ExampleZip", "/Data", Application.persistentDataPath, new User("", "", ""));
    