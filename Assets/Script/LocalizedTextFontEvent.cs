using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using System;
[AddComponentMenu("Localization/Asset/" + nameof(LocalizedTextFontEvent))]
public class LocalizedTextFontEvent : LocalizedAssetEvent<Font, LocalizedAsset<Font>, UnityEventTextFont> {}

[Serializable]
public class UnityEventTextFont : UnityEvent<Font> {}
