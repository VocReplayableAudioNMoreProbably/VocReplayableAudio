using MelonLoader;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using CardAnalogica;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.Windows.Forms;

namespace VoCReplayableAudio
{

    public static class BuildInfo
    {
        public const string Name = "VoCReplayableAudio";
        public const string Description = "Mod for learning japanese through the voice of cards!";
        public const string Author = "WhateverTheYoutubeAccountHas";
        public const string Company = null;
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    public class AudioCardPlayed
    {
        public int AudioIndex { get; set; } = -1;
        public ushort CheckId { get; set; } = 0;
        public float Delay { get; set; } = -1.0f;
        public string TextInfo { get; set; } = null;
        public string TextInfoSceneObjectName { get; set; } = null;
        public AudioCardPlayed( int _AudioIndex = -1, ushort _CheckId = 0, float _Delay = -1.0f, string _TextInfo = null, string _TextInfoSceneObjectName = null)
        {
            AudioIndex = _AudioIndex;
            CheckId = _CheckId;
            Delay = _Delay;
            TextInfo = _TextInfo;
            TextInfoSceneObjectName = _TextInfoSceneObjectName;
        }
    }

    public class ReplayableAudioMod : MelonMod
    {

        public static string TryGetTextOnCard(Transform __0)
        {
            string Ret = null;

            var Test1 = __0.transform.FindChild("CardGraphicObject");
            if (Test1 == null)
            {
                return Ret;
            }

            var Test2 = Test1.FindChild("ImageCardObject");
            if (Test2 == null)
            {
                return Ret;
            }

            var Test3 = Test2.FindChild("CardObject");
            if (Test3 == null)
            {
                return Ret;
            }

            var Test4 = Test3.FindChild("BaseTrans");
            if (Test4 == null)
            {
                return Ret;
            }

            var Test5 = Test4.FindChild("card_deform");
            if (Test5 == null)
            {
                return Ret;
            }

            var Test6 = Test5.FindChild("CardInfo");
            if (Test6 == null)
            {
                return Ret;
            }

            TMPro.TextMeshPro TextMesh = Test6.gameObject.GetComponentInChildren<TMPro.TextMeshPro>();
            Ret = TextMesh.text;

            return Ret;
        }

        public static string CreateCopyPasteString( Transform CardObjectTransform )
        {
            string Ret = "";
            var Test4 = CardObjectTransform.FindChild("BaseTrans");
            if (Test4 == null)
            {
                return Ret;
            }

            var Test5 = Test4.FindChild("card_deform");
            if (Test5 == null)
            {
                return Ret;
            }

            var TitleTransform = Test5.FindChild("CardTitle");
            if (TitleTransform == null)
            {
                return Ret;
            }

            var InfoTransform = Test5.FindChild("CardInfo");
            if (InfoTransform == null)
            {
                return Ret;
            }


            var TitleText = TitleTransform.gameObject.GetComponent<TMPro.TextMeshPro>();
            if ( TitleText != null )
            {
                Ret += "Title: " + TitleText.GetParsedText().Trim();
                Ret += "\n";
            }
            var InfoText = InfoTransform.gameObject.GetComponent<TMPro.TextMeshPro>();
            if ( InfoText != null )
            {
                Ret += "Content: " + InfoText.GetParsedText().Trim();
            }

            return Ret;
        }
        
        public static string CreateCopyPasteStringForFrontBackCollections( Transform CardObjectTransform ) 
        {
            string Ret = "";
            var Test4 = CardObjectTransform.FindChild("BaseTrans");
            if (Test4 == null)
            {
                return Ret;
            }

            string TitleChildName = "CardTitleFront";
            string InfoChildName = "CardInfoFront";

            // Means we are looking on the back of the card.
            if (CardObjectTransform.localRotation.y > 0)
            {
                TitleChildName = "CardTitleBack";
                InfoChildName = "CardInfoBack";
            }

            var TitleTransform = Test4.FindChild(TitleChildName);
            var InfoTransform = Test4.FindChild(InfoChildName);

            if ( TitleTransform != null )
            {
                // Get title text on card.
                var TitleText = TitleTransform.gameObject.GetComponent<TMPro.TextMeshPro>();
                if (TitleText != null)
                {
                    Ret += "Title: " + TitleText.GetParsedText().Trim();
                    Ret += "\n";
                }
            }

            if ( InfoTransform != null )
            {
                var InfoText = InfoTransform.gameObject.GetComponent<TMPro.TextMeshPro>();
                if (InfoText != null)
                {
                    Ret += "Content: " + InfoText.GetParsedText().Trim();
                }
            }

            return Ret;
        }
        
        // Keeping this separate, but it is the same as CreateCopyPasteString essentially.
        public static string CreateCopyPasteStringForMiscellaneousCollections( Transform CardObjectTransform )
        {
            return CreateCopyPasteString( CardObjectTransform );
        }

        public static TMPro.TextMeshPro GetTextMeshProOnCard(Transform Trans)
        {
            TextMeshPro Ret = null;

            var Test1 = Trans.transform.FindChild("CardGraphicObject");
            if (Test1 == null)
            {
                return Ret;
            }

            var Test2 = Test1.FindChild("ImageCardObject");
            if (Test2 == null)
            {
                return Ret;
            }

            var Test3 = Test2.FindChild("CardObject");
            if (Test3 == null)
            {
                return Ret;
            }

            var Test4 = Test3.FindChild("BaseTrans");
            if (Test4 == null)
            {
                return Ret;
            }

            var Test5 = Test4.FindChild("card_deform");
            if (Test5 == null)
            {
                return Ret;
            }

            var Test6 = Test5.FindChild("CardInfo");
            if (Test6 == null)
            {
                return Ret;
            }

            TMPro.TextMeshPro TextMesh = Test6.gameObject.GetComponentInChildren<TMPro.TextMeshPro>();
            return TextMesh;
        }

        // Don't question the implementation.
        public static Transform GetBaseTransOnCard( Transform __0 )
        {
            Transform Ret;

            Ret = __0.transform.FindChild("CardGraphicObject");
            if (Ret == null)
            {
                return Ret;
            }

            Ret = Ret.transform.FindChild("ImageCardObject");
            if (Ret == null)
            {
                return Ret;
            }

            Ret = Ret.transform.FindChild("CardObject");
            if (Ret == null)
            {
                return Ret;
            }

            Ret = Ret.transform.FindChild("BaseTrans");
            if (Ret == null)
            {
                return Ret;
            }

            Ret = Ret.transform.FindChild("card_deform");
            if (Ret == null)
            {
                return Ret;
            }

            return Ret;
        }

        // The audio cards which needs to be updated.
        static List<AudioCardPlayed> AudioCardsToUpdate = new List<AudioCardPlayed>();
        // The audio cards which are ready to be played (is finished).
        static List<AudioCardPlayed> PlayableAudioCards = new List<AudioCardPlayed>();
        static List<ReplayAudioButton> _ReplaybleAudioButtonCaches = new List<ReplayAudioButton>();

        static int _LastVoiceIdPlayed = -1;
        static ushort _LastVoiceCheckIdPlayed = 0;
        static float _LastVoiceDelayPlayed =+ -1.0f;

        [HarmonyPatch(typeof(CardAnalogica.GameSoundManager), "VoicePlay")]
        static class VoicePlayPatch
        {
            static bool Prefix(CardAnalogica.GameSoundManager __instance, bool __result, int __0, ref ushort __1, float __2)
            {
                // These will be used in-case there is no scenario up.
                _LastVoiceIdPlayed = __0;
                _LastVoiceDelayPlayed = __2;
                // Maybe cool test? Should make so each instance always replays...
                // Why did it take me over 100+ hours to try this...
                // Note: We take __1 as ref (voice check id), and set it to 0 so it is never cached as being played.
                __1 = 0;
                
                return true;
            }
        }


        public static void CreateVoiceCardForChoice( int StartIndex, int ChoiceID, int HashCode, Il2CppSystem.Collections.Generic.List<EventCmd> CmdList )
        {
            // When we cannot find anymore EventCmdSoundPlay.
            if ( StartIndex >= CmdList.Count )
            {
                return;
            }

            // Intentional copy of StartIndex, we will increment it outside of this to not be confusing (it is more confusing now).
            for (int CmdIndex = StartIndex; CmdIndex < CmdList.Count; CmdIndex++ )
            {
                var EventCmd = CmdList[ CmdIndex ];
                if ( EventCmd.TryCast<EventCmdSoundPlay>() != null)
                {
                    string ImageKeyName = "EventCmdImageKey" + ChoiceID;
                    var SoundCmd = EventCmd.Cast<EventCmdSoundPlay>();

                    if ( HasAddedCard( SoundCmd.ID, SoundCmd.Delay, ImageKeyName ) )
                    {
                        continue;
                    }

                    AudioCardsToUpdate.Add(new AudioCardPlayed(SoundCmd.ID, 0, SoundCmd.Delay, null, ImageKeyName));
                    break;
                }
            }
        }

        // Data to pipe in
        public static bool HasAddedCard( int SoundID, float Delay, string TextInfo )
        {
            bool AlreadyAdded = AudioCardsToUpdate.FindLast(x => (x.AudioIndex == SoundID) && (x.Delay == Delay) && ( x.TextInfo == TextInfo ) ) != null;
            AlreadyAdded |= PlayableAudioCards.FindLast(x => (x.AudioIndex == SoundID) && (x.Delay == Delay) && (x.TextInfo == TextInfo) ) != null;

            return AlreadyAdded;
        }

        public static bool HasAddedChoiceCard( string TextInfo )
        {
            bool AlreadyAdded = AudioCardsToUpdate.FindLast(x => x.TextInfo == TextInfo) != null;
            AlreadyAdded |= PlayableAudioCards.FindLast(x => x.TextInfo == TextInfo) != null;

            return AlreadyAdded;
        }

        // Finds all the voices and binds them to the cards.
        [HarmonyPatch(typeof(CardAnalogica.EventCmd3DMask), "DoCommand")]
        static class EventCmd3DMask_DoCommandPatch
        {
            static bool Prefix(CardAnalogica.EventCmd3DMask __instance)
            {
                // Return early in-case __instance is null.
                if ( __instance == null )
                {
                    return true;
                }

                // Return early in-case ParentExecutionInfo is null.
                if ( __instance.ParentExcutionInfo == null)
                {
                    return true;
                }

                int CmdListCount = __instance.ParentExcutionInfo.cmdList.Count - 1;
                var CmdList = __instance.ParentExcutionInfo.cmdList;

                // Do all EventCmdChoices first. Then we can "trivially" get all the remainders.
                List<int> SoundCmdIndicies = new List<int>();
                for( int CmdIndex = 0; CmdIndex < CmdListCount; CmdIndex++)
                {
                    var EventCmd = CmdList[CmdIndex];
                    if (EventCmd.TryCast<EventCmdChoices>() != null)
                    {
                        var CmdChoices = EventCmd.Cast<EventCmdChoices>();
                        var ChoicesKeyIDs = new List<int>();
                        // We add the ID from the Choices (will be used to parse events for correct sound + text).
                        foreach (var Choice in CmdChoices.ChoicesList)
                        {
                            ChoicesKeyIDs.Add(Choice.ChoicesID);
                        }

                        // Use the ChoiceID to find the respective ImageAnim, then go up in the list until we find the voice related to it.
                        // Repeat for ChoicesList.Count - n
                        var ChoiceTextInfos = new List<(int, string)>();
                        foreach (var ChoiceKeyID in ChoicesKeyIDs)
                        {
                            // Now we need to find the correct EventImageKey with this ID.
                            // Go backwards until we find the the Event3DImage.
                            for (int Cmd3DImageIndex = CmdIndex; Cmd3DImageIndex >= 0; Cmd3DImageIndex--)
                            {
                                if (CmdList[Cmd3DImageIndex].TryCast<EventCmd3DImage>() != null)
                                {
                                    // We have found one, check validity.
                                    var Cmd3DImageEvent = CmdList[Cmd3DImageIndex].Cast<EventCmd3DImage>();
                                    // Check if has matching KeyID.
                                    if (Cmd3DImageEvent.KeyID == ChoiceKeyID)
                                    {
                                        var FormattedString = ReplaceVariable.m_instance.ReplaceParamString(Cmd3DImageEvent.messageText);
                                        ChoiceTextInfos.Add( (ChoiceKeyID, FormattedString) );
                                        // Break here, can refine later (keep track of indices, next element is gonna iterate unnecessary things).
                                        break;
                                    }
                                }
                            }
                        }

                        if (ChoiceTextInfos.Count > 0)
                        {
                            for (int ImageAnimIndex = CmdIndex; (ImageAnimIndex < CmdListCount); ImageAnimIndex++)
                            {
                                if (CmdList[ImageAnimIndex].TryCast<EventCmd3DImageAnim>() != null)
                                {
                                    var ImageAnimToCheck = CmdList[ImageAnimIndex].Cast<EventCmd3DImageAnim>();
                                    // If the ImageAnim ID is not contained within our ChoicesKeyIDs we continue.
                                    if ( ChoiceTextInfos.Find( x => x.Item1 == ImageAnimToCheck.ID ).Item2 == "" )
                                    {
                                        continue;
                                    }

                                    // We have now found the ImageAnim for the card which will play a voiceline.
                                    // The SoundEventCmd will "always" be above it (-- in CmdList).
                                    // We only exit when ChoiceTextInfos.Count == 0 (or reach out of list for safety).
                                    for (int SoundCmdIndex = ImageAnimIndex; SoundCmdIndex >= 0; SoundCmdIndex--)
                                    {
                                        if (CmdList[SoundCmdIndex].TryCast<EventCmdSoundPlay>() != null)
                                        {
                                            var SoundCmd = CmdList[SoundCmdIndex].Cast<EventCmdSoundPlay>();
                                            // Not a voiceline.
                                            if (SoundCmd.Type != 2)
                                            {
                                                continue;
                                            }

                                            // These are sorted in ascending order, so first will always be the correct one.
                                            string TextForVoiceline = "";
                                            // Take first if we have it.
                                            if (ChoiceTextInfos.Count > 0)
                                            {
                                                // Cannot be null here.
                                                TextForVoiceline = ChoiceTextInfos.Find( (x => x.Item1 == ImageAnimToCheck.ID ) ).Item2;
                                            }
                                            // Otherwise get out of here.
                                            else
                                            {
                                                break;
                                            }

                                            // Checks if this already exists first.
                                            if (HasAddedCard(SoundCmd.ID, SoundCmd.Delay, TextForVoiceline))
                                            {
                                                ChoiceTextInfos.Remove( (ImageAnimToCheck.ID, TextForVoiceline) );
                                                continue;
                                            }


                                            AudioCardsToUpdate.Add(new AudioCardPlayed(SoundCmd.ID, 0, SoundCmd.Delay, TextForVoiceline, null));
                                            ChoiceTextInfos.Remove( ( ImageAnimToCheck.ID, TextForVoiceline ) );

                                            // Break out of the SoundCmd for-loop here if we've added the voiceline.
                                            break;
                                        }
                                    }
                                } // EventCmd3DImageAnim tryCast.
                            } // ImageAnimIndex for-loop
                        } // ChoiceTextInfos.Count
                    } // EventCmdChoices TryCast
                    else if ( EventCmd.TryCast<EventCmdSoundPlay>() != null )
                    {
                        if (EventCmd.Cast<EventCmdSoundPlay>().Type != 2)
                        {
                            continue;
                        }

                        SoundCmdIndicies.Add( CmdIndex );
                    }
                }

                // All indvidiual sound cmds afterwards.
                foreach( var SoundCmdIndex in SoundCmdIndicies )
                {
                    var SoundCmd = CmdList[SoundCmdIndex].Cast<EventCmdSoundPlay>();
                    int ImageAnimKeyID = -1; // -1 is invalid.
                    // Go forward from CmdIndex until we find ImageAnimKey.
                    for (int ImageAnimKeyIndex = SoundCmdIndex; ImageAnimKeyIndex < CmdListCount; ImageAnimKeyIndex++)
                    {
                        if (CmdList[ImageAnimKeyIndex].TryCast<EventCmd3DImageAnim>() != null)
                        {
                            var AnimCmd = CmdList[ImageAnimKeyIndex].Cast<EventCmd3DImageAnim>();
                            ImageAnimKeyID = AnimCmd.ID;
                            // Breakout after we've found the image anim key id.
                            break;
                        }
                    }

                    // Go backwards until we find a EventCmd3DImage that matches our KeyID retrieved earlier.
                    for (int Cmd3DImageIndex = SoundCmdIndex; Cmd3DImageIndex >= 0; Cmd3DImageIndex--)
                    {
                        if (CmdList[Cmd3DImageIndex].TryCast<EventCmd3DImage>() != null)
                        {
                            var ImageCmd = CmdList[Cmd3DImageIndex].Cast<EventCmd3DImage>();
                            if (ImageCmd.KeyID == ImageAnimKeyID)
                            {
                                // This is our connected card!... but it's not over yet, they can share the same name : (.
                                string ImageKeyName = "EventCmdImageKey" + ImageCmd.KeyID;

                                // We have to check if ImageKeyName exist when we wanna play audio.
                                // !!!NOTE MessageText needs to be formatted the same way it is done for the EventCmd3DImage! NOTE!!!
                                // Formatting using their replace param string.
                                var FormattedString = ReplaceVariable.m_instance.ReplaceParamString(ImageCmd.messageText);

                                // Checks if this already exists first.
                                if (HasAddedCard(SoundCmd.ID, SoundCmd.Delay, FormattedString))
                                {
                                    // Break here, because we know we are done.
                                    break;
                                }

                                AudioCardsToUpdate.Add(new AudioCardPlayed(SoundCmd.ID, 0, SoundCmd.Delay, FormattedString, ImageKeyName));

                                // We exit whenever we've found the one.
                                break;
                            }
                        }
                    }
                }

                // Always return true to be nice to other modders ( jk I destroyed the input through raycasting >:-) )
                return true;
            }
        }


        [HarmonyPatch(typeof(ScenarioTextManager), "AddScenarioText")]
        static class ScenarioTextManager_AddScenarioTextPatch
        {
            static void Postfix(CardAnalogica.ScenarioTextManager __instance, CardAnalogica.CmdGraphicLayer __0)
            {
                // Add gui here!
                string TextOnCard = TryGetTextOnCard(__0.transform);
                if ( TextOnCard != null )
                {
                    // Have to check if it has been added here first.
                    bool HasAudioFor = AudioCardsToUpdate.Find(x => ( x.TextInfo == TextOnCard) ) != null || PlayableAudioCards.Find(x => (x.TextInfo == TextOnCard)) != null;
                    if ( HasAudioFor )
                    {
                        Transform MyBoi = GetBaseTransOnCard( __0.transform );
                        if ( MyBoi != null )
                        {
                            ReplayAudioButton NewButton = new ReplayAudioButton( TextOnCard );
                            NewButton.Initialize(MyBoi);

                            _ReplaybleAudioButtonCaches.Add( NewButton );
                        }
                    }
                }
            }
        };

        [HarmonyPatch(typeof(ScenarioTextManager), "DeleteTextCardGroupID")]
        static class ScenarioTextManager_DeleteTextCardGroupIDPatch
        {
            static void Postfix(CardAnalogica.ScenarioTextManager __instance, int __0)
            {
                // Clean up shit here!
                foreach (var AudioButton in _ReplaybleAudioButtonCaches)
                {
                    AudioButton.Destroy();
                }

                _ReplaybleAudioButtonCaches.Clear();
            }
        };

        [HarmonyPatch(typeof(ScenarioTextManager), "DeleteTextCard")]
        static class ScenarioTextManager_DeleteTextCardPatch
        {
            static bool Prefix(CardAnalogica.ScenarioTextManager __instance, string __0)
            {
                // Clean up shit here as well.
                // I think this should be fine... (?).
                foreach (var AudioButton in _ReplaybleAudioButtonCaches)
                {
                    AudioButton.Destroy();
                }

                _ReplaybleAudioButtonCaches.Clear();

                // Always return true here, don't mess with the original code.
                return true;
            }
        };

        public static bool ShouldBlockCallOnClick()
        {
            var Managers = GameObject.Find("Managers");

            if ( !Managers )
            {
                return true;
            }

            Camera SpriteCamera = Managers.transform.GetComponentInChildren<Camera>();
            if ( !SpriteCamera )
            {
                return true;
            }

            // Mouse hovering/pressing graphics.
            var MousePos = Input.mousePosition;
            Ray WorldRay = SpriteCamera.ScreenPointToRay(MousePos);
            var HitInfos = Physics.RaycastAll(WorldRay.origin, WorldRay.direction);
            if (HitInfos.Count > 0)
            {
                var SortedInfo = HitInfos.OrderBy((h1) => h1.distance).ToList();
                var hitInfo = SortedInfo[0];

                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.gameObject.name == "CardMask")
                    {
                        // We naively just take next one (only exists on CardMask Clueless).
                        if (HitInfos.Count > 1)
                        {
                            hitInfo = SortedInfo[1];
                        }
                    }

                    if ( hitInfo.collider != null )
                    {
                        bool WasReplayAudioCard = hitInfo.collider.gameObject.name == "ReplayAudioButton";
                        if ( WasReplayAudioCard )
                        {
                            // Block here (matches graphics)!
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        [HarmonyPatch(typeof(EventCmdTouchWait), "CallOnClick")]
        static class EventCmdTouchWait_CallOnClickPatch
        {
            static bool Prefix(CardAnalogica.EventCmdTouchWait __instance, UnityEngine.GameObject __0)
            {
                return ShouldBlockCallOnClick();
            }
        };

        [HarmonyPatch(typeof(EventCmdChoices), "CallOnClick")]
        static class EventCmdChoices_CallOnClickPatch
        {
            static bool Prefix(CardAnalogica.EventCmdChoices __instance, UnityEngine.GameObject __0)
            {
                // Returns true if it raycasts a ReplayAudioButton.
                return ShouldBlockCallOnClick();
            }
        };

        [HarmonyPatch(typeof(CardAnalogica.MainMenu), "Close")]
        static class MainMenu_ClosePatch
        {
            static bool Prefix(CardAnalogica.MainMenu __instance, bool __result, bool __0)
            {
                // Block close so we can actually copy with CTRL + C in collection.
                if ( Input.GetKey(KeyCode.LeftControl) )
                {
                    return false;
                }

                return true;
            }
        };

        [HarmonyPatch(typeof(CardAnalogica.MainMenu), "OnControlInput")]
        static class MainMenu_OnControlInputPatch
        {
            static bool Prefix(CardAnalogica.MainMenu __instance, UIFramework.ControlStatus.InputInfo __0)
            {
                // Block close so we can actually copy with CTRL + C in collection.
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    return false;
                }

                return true;
            }
        };

        static int ReplayAudioDelay = 0;
        static int ResetAudioDelayCounter = 60;

        public class ReplayAudioButton : MonoBehaviour {
            public GameObject _GameObjectMyButton;
            public GameObject _GameObjectText;
            public string TextOnCardForButton { get; set; } = "";
            public bool IsAnimating { get; set; } = false;
            private Color OriginalBackgroundColor = new Color(18 / 255.0f, 17 / 255.0f, 14 / 255.0f, 1.0f);
            private Color HoverBackgroundColor = new Color(55 / 255.0f, 55 / 255.0f, 55 / 255.0f, 1.0f);
            private Color ClickedBackgroundColor = new Color(85 / 255.0f, 85 / 255.0f, 85 / 255.0f, 1.0f);
            private Color OriginalTextColor = new Color(196 / 255.0f, 184 / 255.0f, 163 / 255.0f);

            public ReplayAudioButton( string _ID )
            {
                TextOnCardForButton = _ID;
            }

            public void Initialize( Transform CardDeformTransform /*maybe used*/ )
            {
                _GameObjectMyButton = new GameObject("ReplayAudioButton");
                _GameObjectMyButton.SetActive(true);

                // Will be replaced by force update down below but w/e.
                Vector2 SizeDelta = new Vector2(1.4f, 0.5f);

                var RectComp = _GameObjectMyButton.AddComponent<RectTransform>();
                RectComp.sizeDelta = SizeDelta;

                var VerticalLayoutComponent = _GameObjectMyButton.AddComponent<VerticalLayoutGroup>();
                var ContentSizeComponent = _GameObjectMyButton.AddComponent<ContentSizeFitter>();
                ContentSizeComponent.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

                var ImageComp = _GameObjectMyButton.AddComponent<UnityEngine.UI.Image>();
                ImageComp.color = OriginalBackgroundColor;

                _GameObjectText = new GameObject("ReplayAudioButtonText");

                var TextMeshComp = _GameObjectText.AddComponent<TextMeshPro>();

                string ReplayText = "Replay";
                switch (GameSystemInfo.instance.Language)
                {
                    case "jp":
                        ReplayText = "リプレイ";
                        break;
                    case "en":
                        ReplayText = "Replay";
                        break;
                    default:
                        break;
                }

                // Get localization here, default to english.
                TextMeshComp.SetText(ReplayText);
                TextMeshComp.color = OriginalTextColor;
                TextMeshComp.fontSize = 4.4f;
                TextMeshComp.raycastTarget = true;
                TextMeshComp.characterSpacing = -0.625f;
                TextMeshComp.horizontalAlignment = HorizontalAlignmentOptions.Center;
                TextMeshComp.verticalAlignment = VerticalAlignmentOptions.Middle;
                TextMeshComp.enableWordWrapping = false;

                var TextRectComp = _GameObjectText.GetComponent<RectTransform>();
                TextRectComp.sizeDelta = SizeDelta;

                // Set parent for text here!
                _GameObjectText.transform.SetParent(_GameObjectMyButton.transform);
                _GameObjectText.transform.localPosition = new Vector3(0, 0, -0.005f);

                LayoutRebuilder.ForceRebuildLayoutImmediate( RectComp );

                var boxCollider = _GameObjectMyButton.AddComponent<BoxCollider>();
                boxCollider.size = RectComp.sizeDelta;
                boxCollider.enabled = true;

                _GameObjectMyButton.layer = 8; /*CmdEngine Layer*/
                _GameObjectText.layer = 8;

                if (CardDeformTransform != null)
                {
                    _GameObjectMyButton.transform.SetParent(CardDeformTransform);
                    _GameObjectMyButton.transform.localPosition = new Vector3(0, 0.0005f, -0.0265f);
                    _GameObjectMyButton.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

                    // Rotate so it looks in the forward of the card (might have to be up).
                    // Also god I hate this so much don't question it.
                    Quaternion RotToCamera = Quaternion.LookRotation(-CardDeformTransform.up);
                    _GameObjectMyButton.transform.rotation = Quaternion.LookRotation(-CardDeformTransform.up);
                }
            }

            public void Destroy()
            {
                DestroyImmediate( _GameObjectMyButton );
            }

            public void OnInteractableGraphics()
            {
                if ( _GameObjectMyButton != null )
                {
                    var ImageComp = _GameObjectMyButton.GetComponent<UnityEngine.UI.Image>();
                    if ( Input.GetMouseButton(0) )
                    {
                        ImageComp.color = ClickedBackgroundColor;
                        // Plays the audio here.
                        PlayAudioCard();
                    }
                    else
                    {
                        ImageComp.color = HoverBackgroundColor;
                    }
                }
            }

            public void OnDefaultGraphics()
            {
                if (_GameObjectMyButton != null)
                {
                    var ImageComp = _GameObjectMyButton.GetComponent<UnityEngine.UI.Image>();
                    ImageComp.color = OriginalBackgroundColor;
                }
            }
        }

        static public void PlayAudioCard()
        {
            if (ReplayAudioDelay >= ResetAudioDelayCounter)
            {
                ReplayAudioDelay = 0;

                if (CardAnalogica.ScenarioTextManager.m_instance.nowViewIndex == 0)
                {
                    return;
                }

                // View index starts at "1", meaning element 0 = 1 in ScenarioTextInfos.
                int CurrentViewIndex = CardAnalogica.ScenarioTextManager.m_instance.nowViewIndex;
                CardAnalogica.ScenarioTextInfo CurrentScenarioTextInfo = CardAnalogica.ScenarioTextManager.m_instance.scenarioTextInfos[CurrentViewIndex - 1];

                string ToFilterOn = TryGetTextOnCard(CurrentScenarioTextInfo.CardObject.transform);
                AudioCardPlayed CurrentAudioCard = null;
                if (ToFilterOn != null)
                {
                    CurrentAudioCard = PlayableAudioCards.Find(x => (x.TextInfo == ToFilterOn));
                }

                if (CurrentAudioCard != null)
                {
                    // Means we actually have one and can play!
                    CardAnalogica.GameSoundManager.m_instance.VoicePlay(CurrentAudioCard.AudioIndex, CurrentAudioCard.CheckId, CurrentAudioCard.Delay);
                }
            }
        }

        public override void OnUpdate()
        {
            // Thinking like this:
            // - We add our GUI button when we have text scenario (and we have a matching card fits that).
            // - We check for our child (to not add more than 1).

            bool ActiveCardInScenario = CardAnalogica.ScenarioTextManager.m_instance.nowViewIndex > 0;
            // Play the latest sound (if no scenario is active).
            if ( Input.GetKeyUp(KeyCode.F3) && _LastVoiceIdPlayed != -1 && !ActiveCardInScenario )
            {
                CardAnalogica.GameSoundManager.m_instance.VoicePlay(_LastVoiceIdPlayed, _LastVoiceCheckIdPlayed, _LastVoiceDelayPlayed);
            }
            // Play the active card voiceline (if scenario is active and it has voiceline).
            else if ( Input.GetKeyUp(KeyCode.F3) && ActiveCardInScenario && CardAnalogica.ScenarioTextManager.m_instance.scenarioTextInfos.Count > 0)
            {
                int CurrentViewIndex = CardAnalogica.ScenarioTextManager.m_instance.nowViewIndex;
                CardAnalogica.ScenarioTextInfo CurrentScenarioTextInfo = CardAnalogica.ScenarioTextManager.m_instance.scenarioTextInfos[CurrentViewIndex - 1];

                string ToFilterOn = TryGetTextOnCard(CurrentScenarioTextInfo.CardObject.transform);
                if ( ToFilterOn != null )
                {
                    AudioCardPlayed PlayableVoiceline = PlayableAudioCards.Find( ToFind => (ToFilterOn.CompareTo(ToFind.TextInfo) == 0) );
                    if ( PlayableVoiceline.AudioIndex != -1 )
                    {
                        CardAnalogica.GameSoundManager.m_instance.VoicePlay(PlayableVoiceline.AudioIndex, 0, PlayableVoiceline.Delay);
                    }
                }
            }

            // If we have any active cards in scenario we add playable cards.
            if ( ActiveCardInScenario )
            {
                // Decouple names name and stuff to AudioCardsToUpdate.
                foreach (var ScenarioText in CardAnalogica.ScenarioTextManager.m_instance.scenarioTextInfos)
                {
                    // Find first card that matches the info scene object name.
                    // We should move these to "done audio cards" which will be serialized/used when playing cards etc.
                    if ( ScenarioText.CardObject )
                    {
                        var TextOnCard = TryGetTextOnCard(ScenarioText.CardObject.transform);
                        if (TextOnCard != null)
                        {
                            AudioCardPlayed CurrentAudioCard = AudioCardsToUpdate.Find(x => (x.TextInfo == TextOnCard));
                            if (CurrentAudioCard != null)
                            {
                                // Change to another struct for "finished audio cards". But testing for now.
                                // Add to playable audio card
                                PlayableAudioCards.Add(CurrentAudioCard);
                                // Remove from audio cards to update.
                                AudioCardsToUpdate.Remove(CurrentAudioCard);
                            }
                        }
                    }
                }
            }

            // SECTION FOR HIGHLIGHTING SCENARIOTEXT BUTTONS.
            var Managers = GameObject.Find("Managers");
            if ( Managers != null )
            {
                // Raycasting on SpriteCamera stuff (ScenarioText cards).
                Camera SpriteCamera = Managers.transform.GetComponentInChildren<Camera>();
                if ( SpriteCamera != null )
                {
                    // Mouse hovering/pressing graphics.
                    var MousePos = Input.mousePosition;
                    Ray WorldRay = SpriteCamera.ScreenPointToRay(MousePos);
                    var HitInfos = Physics.RaycastAll(WorldRay.origin, WorldRay.direction);
                    if (HitInfos.Count > 0)
                    {
                        var SortedInfo = HitInfos.OrderBy((h1) => h1.distance).ToList();
                        bool HoveringAButton = false;
                        var hitInfo = SortedInfo[0];

                        if (hitInfo.collider != null)
                        {
                            if (hitInfo.collider.gameObject.name == "CardMask")
                            {
                                // We naively just take next one (only exists on CardMask Clueless, copy pasted code).
                                if (HitInfos.Count > 1)
                                {
                                    hitInfo = SortedInfo[1];
                                }
                            }

                            if ( hitInfo.collider != null )
                            {
                                // Graphics for highlighting the replay button.
                                // Early out (we do not allow highlights for secondary ReplayButtons).
                                bool WasReplayAudioCard = hitInfo.collider.gameObject.name == "ReplayAudioButton";
                                if ( WasReplayAudioCard )
                                {
                                    // Start animating.
                                    int CurrentViewIndex = CardAnalogica.ScenarioTextManager.m_instance.nowViewIndex;
                                    CardAnalogica.ScenarioTextInfo CurrentScenarioTextInfo = CardAnalogica.ScenarioTextManager.m_instance.scenarioTextInfos[CurrentViewIndex - 1];

                                    string ToFilterOn = TryGetTextOnCard(CurrentScenarioTextInfo.CardObject.transform);

                                    ReplayAudioButton replayAudioButton = _ReplaybleAudioButtonCaches.Find(ToFind => (ToFilterOn.CompareTo(ToFind.TextOnCardForButton) == 0));
                                    if (replayAudioButton.TextOnCardForButton != "")
                                    {
                                        HoveringAButton |= true;
                                        replayAudioButton.OnInteractableGraphics();
                                    }
                                }
                                // Copy Title + Content.
                                else if (hitInfo.collider.gameObject.name == "CardObject")
                                {
                                    // Might as well check this here.
                                    if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
                                    {
                                        var TextToCopy = CreateCopyPasteString(hitInfo.collider.transform);
                                        if (TextToCopy != "")
                                        {
                                            Clipboard.SetText(TextToCopy);
                                        }
                                    }
                                }
                            }
                        }

                        // Very lazy stuff but should be fine (unless the game does like 50+ scenario texts in a row.)
                        // But in that case it's messed up anyways.
                        // Follow up note:
                        // It was not fine, there is a huge freeze at the end of the game because it loads like 2k events
                        // but I can't be bothered to figure out a better way, I didn't mind it, hopefully you don't to.
                        if (!HoveringAButton)
                        {
                            foreach (var ReplayAudioData in _ReplaybleAudioButtonCaches)
                            {
                                ReplayAudioData.OnDefaultGraphics();
                            }
                        }
                    }
                }
            }

            // SECTION FOR COPYING TEXT IN COLLECTION.
            var MainCamera = GameObject.Find("GUICamera");
            if (MainCamera != null)
            {
                Camera MainCameraComponent = MainCamera.GetComponent<Camera>();
                if (MainCameraComponent != null)
                {
                    // Mouse hovering/pressing graphics.
                    var MousePos = Input.mousePosition;
                    Ray WorldRay = MainCameraComponent.ScreenPointToRay(MousePos);
                    // Need to fix layer here.
                    var HitInfos = Physics.RaycastAll(WorldRay.origin, WorldRay.direction);
                    if (HitInfos.Count > 0)
                    {
                        var SortedInfo = HitInfos.OrderBy((h1) => h1.distance).ToList();
                        bool HoveringAButton = false;
                        var hitInfo = SortedInfo[0];

                        if (hitInfo.collider != null)
                        {
                            if (hitInfo.collider.gameObject.name == "CardMask")
                            {
                                // We naively just take next one (only exists on CardMask).
                                if (HitInfos.Count > 1)
                                {
                                    hitInfo = SortedInfo[1];
                                }
                            }

                            if (hitInfo.collider != null)
                            {
                                // Copy Title + Content.
                                if (hitInfo.collider.gameObject.name == "CardObject")
                                {
                                    // Might as well check this here.
                                    if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
                                    {
                                        if (hitInfo.collider.gameObject.name == "CardObject")
                                        {
                                            var TextToCopy = CreateCopyPasteStringForFrontBackCollections(hitInfo.collider.transform);
                                            if (TextToCopy != "")
                                            {
                                                Clipboard.SetText(TextToCopy);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // Might as well check this here.
                                    if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
                                    {
                                        // For items/skills etc.
                                        var TextToCopy = CreateCopyPasteStringForMiscellaneousCollections(hitInfo.collider.transform);
                                        if (TextToCopy != "")
                                        {
                                            Clipboard.SetText(TextToCopy);
                                        }
                                    }
                                }
                            }
                        }

                        // Very lazy stuff but should be fine (unless the game does like 50+ scenario texts in a row).
                        // But in that case it's messed up anyways.
                        // Wait is this the second time I read this comment? Wow, imagine making a resuable function :joy: :ok_hand: :fire: :100:
                        if (!HoveringAButton)
                        {
                            foreach (var ReplayAudioData in _ReplaybleAudioButtonCaches)
                            {
                                ReplayAudioData.OnDefaultGraphics();
                            }
                        }
                    }
                }
            }
            ReplayAudioDelay++;
        }

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Imagine learning languages through video games.");
        }
    }
}
