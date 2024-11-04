using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using System;

[Serializable]
public class UnityEventText : UnityEvent<Text> {}

[AddComponentMenu("Localization/Asset/LocalizedTextFontEvent")]
public class LocalizedToggleFont : LocalizedAssetEvent<Font, LocalizedAsset<Font>, UnityEventTextFont> {}

