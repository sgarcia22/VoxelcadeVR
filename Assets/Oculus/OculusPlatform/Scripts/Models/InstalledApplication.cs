// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
  using System;
  using System.Collections;
  using Oculus.Platform.Models;
  using System.Collections.Generic;
  using UnityEngine;

  public class InstalledApplication
  {
    public readonly string ApplicationId;
    public readonly string PackageName;


    public InstalledApplication(IntPtr o)
    {
      ApplicationId = CAPI.ovr_InstalledApplication_GetApplicationId(o);
      PackageName = CAPI.ovr_InstalledApplication_GetPackageName(o);
    }
  }

  public class InstalledApplicationList : DeserializableList<InstalledApplication> {
    public InstalledApplicationList(IntPtr a) {
      var count = (int)CAPI.ovr_InstalledApplicationArray_GetSize(a);
      _Data = new List<InstalledApplication>(count);
      for (int i = 0; i < count; i++) {
        _Data.Add(new InstalledApplication(CAPI.ovr_InstalledApplicationArray_GetElement(a, (UIntPtr)i)));
      }

    }

  }
}
