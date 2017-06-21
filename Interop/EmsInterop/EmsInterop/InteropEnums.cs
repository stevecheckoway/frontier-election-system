// -----------------------------------------------------------------------------
// <copyright file="InteropEnums.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the InteropEnums class.
// </summary>
// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Interop
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     Element type enum
    /// </summary>
    public enum ElementType
    {
        /// <summary>
        ///     Candidate element type
        /// </summary>
        Candidate,

        /// <summary>
        ///     Contest element type
        /// </summary>
        Contest,

        /// <summary>
        ///     Proposal element type
        /// </summary>
        Proposal,

        /// <summary>
        ///     Option element type
        /// </summary>
        Option
    }

    /// <summary>
    ///     Item position enum
    /// </summary>
    public enum ItemPosition
    {
        /// <summary>
        ///     Coordinates of the item are interpreted as relative to the 
        ///     upper-left corner of the page.
        /// </summary>
        Absolute,

        /// <summary>
        ///     coordinates of the item are interpreted as relative to the 
        ///     lower-left corner of the previous element in the natural flow of 
        ///     the document. This flow is given by the order in which items are 
        ///     drawn on the page.
        /// </summary>
        RelativeDown,

        /// <summary>
        ///     Coordinates of the item are interpreted as relative to the 
        ///     upper-right corner of the previous element in the natural flow 
        ///     of the document. This flow is given by the order in which items 
        ///     are drawn on the page.
        /// </summary>
        RelativeRight,

        /// <summary>
        ///     Coordinates of the item are interpreted as relative to the \
        ///     upper-left corner of the parent or container element 
        ///     of the item.
        /// </summary>
        Parent
    }

    /// <summary>
    ///     Page origin enum
    /// </summary>
    public enum PageOrigin
    {
        /// <summary>
        ///     Sets the coordinates origin at the upper left corner of 
        ///     the page:
        ///     (0,0) ------> [X]
        ///           |
        ///           |
        ///           |
        ///           V [Y]       
        /// </summary>
        UpperLeft,

        /// <summary>
        ///     Sets the coordinates origin at the lower left corner 
        ///     of the page: 
        ///           ^ [Y]
        ///           |
        ///           |
        ///           |
        ///     (0,0) ------> [X] 
        /// </summary>
        LowerLeft
    }

    /// <summary>
    ///     File type enum
    /// </summary>
    public enum FileType
    {
        /// <summary>
        ///     PDF (portable document format)
        /// </summary>
        Pdf,

        /// <summary>
        ///     SVG (scalable vector graphics)
        /// </summary>
        Svg,

        /// <summary>
        ///     XPS (XML paper specification)
        /// </summary>
        Xps
    }

    /// <summary>
    ///     For a full reference please refer to
    ///     the Visio SDK documentation
    /// </summary>
    public enum LangId
    {
        /// <summary>
        ///     Afrikaans language id
        /// </summary>
        Afrikaans = 1078,

        /// <summary>
        ///     Albanian language id
        /// </summary>
        Albanian = 1052,

        /// <summary>
        ///     Amharic language id
        /// </summary>
        Amharic = 1118,

        /// <summary>
        ///     Arabic language id
        /// </summary>
        Arabic = 1025,

        /// <summary>
        ///     Armenian language id
        /// </summary>
        Armenian = 1067,

        /// <summary>
        ///     Assamese language id
        /// </summary>
        Assamese = 1101,

        /// <summary>
        ///     Azeri Cyrillic language id
        /// </summary>
        Azeri_Cyrillic = 2092,

        /// <summary>
        ///     Azeri Latin language id
        /// </summary>
        Azeri_Latin = 1068,

        /// <summary>
        ///     Basque language id
        /// </summary>
        Basque = 1069,

        /// <summary>
        ///     Belarusian language id
        /// </summary>
        Belarusian = 1059,

        /// <summary>
        ///     Bengali language id
        /// </summary>
        Bengali = 1093,

        /// <summary>
        ///     Bulgarian language id
        /// </summary>
        Bulgarian = 1026,

        /// <summary>
        ///     Catalan language id
        /// </summary>
        Catalan = 1027,

        /// <summary>
        ///     Cherokee language id
        /// </summary>
        Cherokee = 1116,

        /// <summary>
        ///     Chinese Simplified language id
        /// </summary>
        Chinese_Simplified = 2052,

        /// <summary>
        ///     Chinese Traditional language id
        /// </summary>
        Chinese_Traditional = 1028,

        /// <summary>
        ///     Croatian language id
        /// </summary>
        Croatian = 1050,

        /// <summary>
        ///     Czech language id
        /// </summary>
        Czech = 1029,

        /// <summary>
        ///     Danish language id
        /// </summary>
        Danish = 1030,

        /// <summary>
        ///     Dhivehi language id
        /// </summary>
        Dhivehi = 1125,

        /// <summary>
        ///     Dutch language id
        /// </summary>
        Dutch = 1043,

        /// <summary>
        ///     Edo language id
        /// </summary>
        Edo = 1126,

        /// <summary>
        ///     English Australian language id
        /// </summary>
        English_Australian = 3081,

        /// <summary>
        ///     Enlish Canadian language id
        /// </summary>
        English_Canadian = 4105,

        /// <summary>
        ///     English UK language id
        /// </summary>
        English_UK = 2057,

        /// <summary>
        ///     English US language id
        /// </summary>
        English_US = 1033,

        /// <summary>
        ///     Estonian language id
        /// </summary>
        Estonian = 1061,

        /// <summary>
        ///     Faeroese language id 
        /// </summary>
        Faeroese = 1080,

        /// <summary>
        ///     Filipino language id 
        /// </summary>
        Filipino = 1124,

        /// <summary>
        ///     Finnish language id
        /// </summary>
        Finnish = 1035,

        /// <summary>
        ///     French language id
        /// </summary>
        French = 1036,

        /// <summary>
        ///     French Canadian language id 
        /// </summary>
        French_Canadian = 3084,

        /// <summary>
        ///     Frisian language id
        /// </summary>
        Frisian = 1122,

        /// <summary>
        ///     Fulfulde language id
        /// </summary>
        Fulfulde = 1127,

        /// <summary>
        ///     Galician language id
        /// </summary>
        Galician = 1110,

        /// <summary>
        ///     Georgian language id
        /// </summary>
        Georgian = 1079,

        /// <summary>
        ///     German language id
        /// </summary>
        German = 1031,

        /// <summary>
        ///     German Austrian language id
        /// </summary>
        German_Austrian = 3079,

        /// <summary>
        ///     German Swiss language id 
        /// </summary>
        German_Swiss = 2055,

        /// <summary>
        ///     Greek language id
        /// </summary>
        Greek = 1032,

        /// <summary>
        ///     Gujarati language id
        /// </summary>
        Gujarati = 1095,

        /// <summary>
        ///     Hausa language id
        /// </summary>
        Hausa = 1128,

        /// <summary>
        ///     Hawaiian language id
        /// </summary>
        Hawaiian = 1141,

        /// <summary>
        ///     Hebrew language id
        /// </summary>
        Hebrew = 1037,

        /// <summary>
        ///     Hindi language id
        /// </summary>
        Hindi = 1081,

        /// <summary>
        ///     Hungarian language id 
        /// </summary>
        Hungarian = 1038,

        /// <summary>
        ///     Ibibio language id 
        /// </summary>
        Ibibio = 1129,

        /// <summary>
        ///     Icelandic language id 
        /// </summary>
        Icelandic = 1039,

        /// <summary>
        ///     Igbo language id
        /// </summary>
        Igbo = 1136,

        /// <summary>
        ///     Indonesian language id
        /// </summary>
        Indonesian = 1057,

        /// <summary>
        ///     Inuktitut language id
        /// </summary>
        Inuktitut = 1117,

        /// <summary>
        ///     Italian language id
        /// </summary>
        Italian = 1040,

        /// <summary>
        ///     Japanese language id
        /// </summary>
        Japanese = 1041,

        /// <summary>
        ///     Kannada language id
        /// </summary>
        Kannada = 1099,

        /// <summary>
        ///     Kanuri language id
        /// </summary>
        Kanuri = 1137,

        /// <summary>
        ///     Kashmiri language id
        /// </summary>
        Kashmiri = 2144,

        /// <summary>
        ///     Kashmiri Arabic language id
        /// </summary>
        Kashmiri_Arabic = 1120,

        /// <summary>
        ///     Kazakh language id
        /// </summary>
        Kazakh = 1087,

        /// <summary>
        ///     Kyrgyz language id 
        /// </summary>
        Kyrgyz = 1088,

        /// <summary>
        ///     Konkani language id
        /// </summary>
        Konkani = 1111,

        /// <summary>
        ///     Korean language id 
        /// </summary>
        Korean = 1042,

        /// <summary>
        ///     Latin language id
        /// </summary>
        Latin = 1142,

        /// <summary>
        ///     Latvian language id
        /// </summary>
        Latvian = 1062,

        /// <summary>
        ///     Lithuanian language id 
        /// </summary>
        Lithuanian = 1063,

        /// <summary>
        ///     Macedonian FYROM language id
        /// </summary>
        Macedonian_FYROM = 1071,

        /// <summary>
        ///     Malay language id 
        /// </summary>
        Malay = 1086,

        /// <summary>
        ///     Malayalam language id
        /// </summary>
        Malayalam = 1100,

        /// <summary>
        ///     Maltese language id 
        /// </summary>
        Maltese = 1082,

        /// <summary>
        ///     Manipuri language id
        /// </summary>
        Manipuri = 1112,

        /// <summary>
        ///     Marathi language id
        /// </summary>
        Marathi = 1102,

        /// <summary>
        ///     Mongolian language id 
        /// </summary>
        Mongolian = 1104,

        /// <summary>
        ///     Nepali language id 
        /// </summary>
        Nepali = 1121,

        /// <summary>
        ///     Norwegian Bokmal language id
        /// </summary>
        Norwegian_Bokmal = 1044,

        /// <summary>
        ///     Norwegian Nynorsk language id
        /// </summary>
        Norwegian_Nynorsk = 2068,

        /// <summary>
        ///     Oriya language id
        /// </summary>
        Oriya = 1096,

        /// <summary>
        ///     Oromo language id 
        /// </summary>
        Oromo = 1138,

        /// <summary>
        ///     Pashto language id
        /// </summary>
        Pashto = 1123,

        /// <summary>
        ///     Persian language id
        /// </summary>
        Persian = 1065,

        /// <summary>
        ///     Polish language id
        /// </summary>
        Polish = 1045,

        /// <summary>
        ///     Portuguese Brazil language id 
        /// </summary>
        Portuguese_Brazil = 1046,

        /// <summary>
        ///     Portuguese Portugal language id
        /// </summary>
        Portuguese_Portugal = 2070,

        /// <summary>
        ///     Punjabi language id 
        /// </summary>
        Punjabi = 1094,

        /// <summary>
        ///     Romanian language id
        /// </summary>
        Romanian = 1048,

        /// <summary>
        ///     Russian language id
        /// </summary>
        Russian = 1049,

        /// <summary>
        ///     Sanskrit language id 
        /// </summary>
        Sanskrit = 1103,

        /// <summary>
        ///     Serbian Cyrillic language id
        /// </summary>
        Serbian_Cyrillic = 3098,

        /// <summary>
        ///     Serbian Latin language id
        /// </summary>
        Serbian_Latin = 2074,

        /// <summary>
        ///     Sindhi language id
        /// </summary>
        Sindhi = 1113,

        /// <summary>
        ///     Sinhalese language id 
        /// </summary>
        Sinhalese = 1115,

        /// <summary>
        ///     Slovak language id
        /// </summary>
        Slovak = 1051,

        /// <summary>
        ///     Slovenian language id
        /// </summary>
        Slovenian = 1060,

        /// <summary>
        ///     Somali language id
        /// </summary>
        Somali = 1143,

        /// <summary>
        ///     Spanish language id
        /// </summary>
        Spanish = 3082,

        /// <summary>
        ///     Swahili language id
        /// </summary>
        Swahili = 1089,

        /// <summary>
        ///     Swedish language id
        /// </summary>
        Swedish = 1053,

        /// <summary>
        ///     Syriac language id
        /// </summary>
        Syriac = 1114,

        /// <summary>
        ///     Tajik language id
        /// </summary>
        Tajik = 1064,

        /// <summary>
        ///     Tamazight Arabic language id
        /// </summary>
        Tamazight_Arabic = 1119,

        /// <summary>
        ///     Tamazight Latin language id
        /// </summary>
        Tamazight_Latin = 2143,

        /// <summary>
        ///     Tamil language id
        /// </summary>
        Tamil = 1097,

        /// <summary>
        ///     Tatar language id
        /// </summary>
        Tatar = 1092,

        /// <summary>
        ///     Telugu language id
        /// </summary>
        Telugu = 1098,

        /// <summary>
        ///     Thai language id
        /// </summary>
        Thai = 1054,

        /// <summary>
        ///     Tigrigna Ethiopia language id 
        /// </summary>
        Tigrigna_Ethiopia = 1139,

        /// <summary>
        ///     Tigrigna Eritrea language id
        /// </summary>
        Tigrigna_Eritrea = 2163,

        /// <summary>
        ///     Turkish language id
        /// </summary>
        Turkish = 1055,

        /// <summary>
        ///     Turkmen language id
        /// </summary>
        Turkmen = 1090,

        /// <summary>
        ///     Ukranian language id
        /// </summary>
        Ukrainian = 1058,

        /// <summary>
        ///     Urdu language id
        /// </summary>
        Urdu = 1056,

        /// <summary>
        ///     Uzbek Cyrillic language id 
        /// </summary>
        Uzbek_Cyrillic = 2115,

        /// <summary>
        ///     Uzbek Latin language id
        /// </summary>
        Uzbek_Latin = 1091,

        /// <summary>
        ///     Vietnamese language id
        /// </summary>
        Vietnamese = 1066,

        /// <summary>
        ///     Yi  language id 
        /// </summary>
        Yi = 1144,

        /// <summary>
        ///     Yiddish language id
        /// </summary>
        Yiddish = 1085,

        /// <summary>
        ///     Yoruba language id
        /// </summary>
        Yoruba = 1130
    }
}
