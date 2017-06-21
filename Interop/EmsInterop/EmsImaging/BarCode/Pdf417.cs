// -----------------------------------------------------------------------------
// <copyright file="Pdf417.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Pdf417 class.
// </summary>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Imaging.BarCode
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using java.math;

    #endregion

    /// <summary>
    ///     Class for creating Pdf417 formatted barcodes.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Added documentation header
    /// </revision>
    public class Pdf417
    {
        #region Definitions

        /// <summary>
        /// ascii [9..127]
        /// each ascii char is represented as follows:
        ///     {bitwise tables, row number}
        ///     
        ///         1       2       4       8         - table
        /// value  U-case  L-case  Mixed  Punct
        ///  0      A       a       0       ;
        ///  1      B       b       1       lt
        ///  2      C       c       2       gt
        ///  3      D       d       3       @
        ///  4      E       e       4       [
        ///  5      F       f       5       \
        /// ...
        /// 
        /// for instance:
        /// a -> value 97 -> index (97 - 9) -> index 89
        /// -> Characters[89] -> {2, 0} -> table(s) 2, value 0
        /// 
        /// Those characters with {0, 0} are characters that are not represented
        /// on the table and thus are ignored
        /// </summary>
        private static readonly int[,] Characters = 
        {
            {12, 12}, {08, 15}, {00, 00}, {00, 00}, {12, 11},  /* HT LF CR */
            {00, 00}, {00, 00}, {00, 00}, {00, 00}, {00, 00},
            {00, 00}, {00, 00}, {00, 00}, {00, 00}, {00, 00},
            {00, 00}, {00, 00}, {00, 00}, {00, 00}, {00, 00},
            {00, 00}, {00, 00}, {00, 00}, {07, 26}, {08, 10},  /* SP ! */
            {08, 20}, {04, 15}, {12, 18}, {04, 21}, {04, 10},  /* " # $ % & */
            {08, 28}, {08, 23}, {08, 24}, {12, 22}, {04, 20},  /* ' ( ) * + */
            {12, 13}, {12, 16}, {12, 17}, {12, 19}, {04, 00},  /* , - . / 0 */
            {04, 01}, {04, 02}, {04, 03}, {04, 04}, {04, 05},  /* 1 2 3 4 5 */
            {04, 06}, {04, 07}, {04, 08}, {04, 09}, {12, 14},  /* 6 7 8 9 : */
            {08, 00}, {08, 01}, {04, 23}, {08, 02}, {08, 25},  /* ; < = > ? */
            {08, 03}, {01, 00}, {01, 01}, {01, 02}, {01, 03},  /* @ A B C D */
            {01, 04}, {01, 05}, {01, 06}, {01, 07}, {01, 08},  /* E F G H I */
            {01, 09}, {01, 10}, {01, 11}, {01, 12}, {01, 13},  /* J K L M N */
            {01, 14}, {01, 15}, {01, 16}, {01, 17}, {01, 18},  /* O P Q R S */
            {01, 19}, {01, 20}, {01, 21}, {01, 22}, {01, 23},  /* T U V W X */
            {01, 24}, {01, 25}, {08, 04}, {08, 05}, {08, 06},  /* Y Z [ \ ] */
            {04, 24}, {08, 07}, {08, 08}, {02, 00}, {02, 01},  /* ^ _ ` a b */
            {02, 02}, {02, 03}, {02, 04}, {02, 05}, {02, 06},  /* c d e f g */
            {02, 07}, {02, 08}, {02, 09}, {02, 10}, {02, 11},  /* h i j k l */
            {02, 12}, {02, 13}, {02, 14}, {02, 15}, {02, 16},  /* m n o p q */
            {02, 17}, {02, 18}, {02, 19}, {02, 20}, {02, 21},  /* r s t u v */
            {02, 22}, {02, 23}, {02, 24}, {02, 25}, {08, 26},  /* w x y z { */
            {08, 21}, {08, 27}, {08, 09}                       /* | } ~ */
        };

        /// <summary>
        ///     These are the coefficients used by the Reed Salomon codes 
        ///     employed for error correction and detection.
        /// 
        ///     Security levels are in the range 0 to 8, having 2^(n+1) 
        ///     coefficients for each level
        /// </summary>
        private static readonly int[][] Coefficients = 
        {
               /* level 0 (2^(0+1) = 2 coefficients) */
               new int[] { 027, 917 },
               /* level 1 (2^(1+1) = 4 coefficients) */
               new int[] { 522, 568, 723, 809 },
               /* level 2 (2^(2+1) = 8 coefficients) */
               new int[] { 237, 308, 436, 284, 646, 653, 428, 379 },
               /* level 3 (2^(3+1) = 16 coefficients) */
               new int[] { 274, 562, 232, 755, 599, 524, 801, 132, 295, 116, 
                           442, 428, 295, 042, 176, 065 },
               /* level 4 (2^(4+1) = 32 coefficients) */
               new int[] { 361, 575, 922, 525, 176, 586, 640, 321, 536, 742, 
                           677, 742, 687, 284, 193, 517, 273, 494, 263, 147, 
                           593, 800, 571, 320, 803, 133, 231, 390, 685, 330, 
                           063, 410 },
               /* level 5 (2^(5+1) = 64 coefficients) */
               new int[] { 539, 422, 006, 093, 862, 771, 453, 106, 610, 287, 
                           107, 505, 733, 877, 381, 612, 723, 476, 462, 172, 
                           430, 609, 858, 822, 543, 376, 511, 400, 672, 762, 
                           283, 184, 440, 035, 519, 031, 460, 594, 225, 535, 
                           517, 352, 605, 158, 651, 201, 488, 502, 648, 733, 
                           717, 083, 404, 097, 280, 771, 840, 629, 004, 381, 
                           843, 623, 264, 543 },
               /* level 6 (2^(6+1) = 128 coefficients) */
               new int[] { 521, 310, 864, 547, 858, 580, 296, 379, 053, 779, 
                           897, 444, 400, 925, 749, 415, 822, 093, 217, 208, 
                           928, 244, 583, 620, 246, 148, 447, 631, 292, 908, 
                           490, 704, 516, 258, 457, 907, 594, 723, 674, 292, 
                           272, 096, 684, 432, 686, 606, 860, 569, 193, 219, 
                           129, 186, 236, 287, 192, 775, 278, 173, 040, 379, 
                           712, 463, 646, 776, 171, 491, 297, 763, 156, 732, 
                           095, 270, 447, 090, 507, 048, 228, 821, 808, 898, 
                           784, 663, 627, 378, 382, 262, 380, 602, 754, 336, 
                           089, 614, 087, 432, 670, 616, 157, 374, 242, 726, 
                           600, 269, 375, 898, 845, 454, 354, 130, 814, 587, 
                           804, 034, 211, 330, 539, 297, 827, 865, 037, 517, 
                           834, 315, 550, 086, 801, 004, 108, 539 },
               /* level 7 (2^(7+1) = 256 coefficients) */
               new int[] { 524, 894, 075, 766, 882, 857, 074, 204, 082, 586, 
                           708, 250, 905, 786, 138, 720, 858, 194, 311, 913, 
                           275, 190, 375, 850, 438, 733, 194, 280, 201, 280, 
                           828, 757, 710, 814, 919, 089, 068, 569, 011, 204, 
                           796, 605, 540, 913, 801, 700, 799, 137, 439, 418, 
                           592, 668, 353, 859, 370, 694, 325, 240, 216, 257, 
                           284, 549, 209, 884, 315, 070, 329, 793, 490, 274, 
                           877, 162, 749, 812, 684, 461, 334, 376, 849, 521, 
                           307, 291, 803, 712, 019, 358, 399, 908, 103, 511, 
                           051, 008, 517, 225, 289, 470, 637, 731, 066, 255, 
                           917, 269, 463, 830, 730, 433, 848, 585, 136, 538, 
                           906, 090, 002, 290, 743, 199, 655, 903, 329, 049, 
                           802, 580, 355, 588, 188, 462, 010, 134, 628, 320, 
                           479, 130, 739, 071, 263, 318, 374, 601, 192, 605, 
                           142, 673, 687, 234, 722, 384, 177, 752, 607, 640, 
                           455, 193, 689, 707, 805, 641, 048, 060, 732, 621, 
                           895, 544, 261, 852, 655, 309, 697, 755, 756, 060, 
                           231, 773, 434, 421, 726, 528, 503, 118, 049, 795, 
                           032, 144, 500, 238, 836, 394, 280, 566, 319, 009, 
                           647, 550, 073, 914, 342, 126, 032, 681, 331, 792, 
                           620, 060, 609, 441, 180, 791, 893, 754, 605, 383, 
                           228, 749, 760, 213, 054, 297, 134, 054, 834, 299, 
                           922, 191, 910, 532, 609, 829, 189, 020, 167, 029, 
                           872, 449, 083, 402, 041, 656, 505, 579, 481, 173, 
                           404, 251, 688, 095, 497, 555, 642, 543, 307, 159, 
                           924, 558, 648, 055, 497, 010 },
               /* level 8 (2^(8+1) = 512 coefficients) */
               new int[] { 352, 077, 373, 504, 035, 599, 428, 207, 409, 574, 
                           118, 498, 285, 380, 350, 492, 197, 265, 920, 155, 
                           914, 299, 229, 643, 294, 871, 306, 088, 087, 193, 
                           352, 781, 846, 075, 327, 520, 435, 543, 203, 666, 
                           249, 346, 781, 621, 640, 268, 794, 534, 539, 781, 
                           408, 390, 644, 102, 476, 499, 290, 632, 545, 037, 
                           858, 916, 552, 041, 542, 289, 122, 272, 383, 800, 
                           485, 098, 752, 472, 761, 107, 784, 860, 658, 741, 
                           290, 204, 681, 407, 855, 085, 099, 062, 482, 180, 
                           020, 297, 451, 593, 913, 142, 808, 684, 287, 536, 
                           561, 076, 653, 899, 729, 567, 744, 390, 513, 192, 
                           516, 258, 240, 518, 794, 395, 768, 848, 051, 610, 
                           384, 168, 190, 826, 328, 596, 786, 303, 570, 381, 
                           415, 641, 156, 237, 151, 429, 531, 207, 676, 710, 
                           089, 168, 304, 402, 040, 708, 575, 162, 864, 229, 
                           065, 861, 841, 512, 164, 477, 221, 092, 358, 785, 
                           288, 357, 850, 836, 827, 736, 707, 094, 008, 494, 
                           114, 521, 002, 499, 851, 543, 152, 729, 771, 095, 
                           248, 361, 578, 323, 856, 797, 289, 051, 684, 466, 
                           533, 820, 669, 045, 902, 452, 167, 342, 244, 173, 
                           035, 463, 651, 051, 699, 591, 452, 578, 037, 124, 
                           298, 332, 552, 043, 427, 119, 662, 777, 475, 850, 
                           764, 364, 578, 911, 283, 711, 472, 420, 245, 288, 
                           594, 394, 511, 327, 589, 777, 699, 688, 043, 408, 
                           842, 383, 721, 521, 560, 644, 714, 559, 062, 145, 
                           873, 663, 713, 159, 672, 729, 624, 059, 193, 417, 
                           158, 209, 563, 564, 343, 693, 109, 608, 563, 365, 
                           181, 772, 677, 310, 248, 353, 708, 410, 579, 870, 
                           617, 841, 632, 860, 289, 536, 035, 777, 618, 586, 
                           424, 833, 077, 597, 346, 269, 757, 632, 695, 751, 
                           331, 247, 184, 045, 787, 680, 018, 066, 407, 369, 
                           054, 492, 228, 613, 830, 922, 437, 519, 644, 905, 
                           789, 420, 305, 441, 207, 300, 892, 827, 141, 537, 
                           381, 662, 513, 056, 252, 341, 242, 797, 838, 837, 
                           720, 224, 307, 631, 061, 087, 560, 310, 756, 665, 
                           397, 808, 851, 309, 473, 795, 378, 031, 647, 915, 
                           459, 806, 590, 731, 425, 216, 548, 249, 321, 881, 
                           699, 535, 673, 782, 210, 815, 905, 303, 843, 922, 
                           281, 073, 469, 791, 660, 162, 498, 308, 155, 422, 
                           907, 817, 187, 062, 016, 425, 535, 336, 286, 437, 
                           375, 273, 610, 296, 183, 923, 116, 667, 751, 353, 
                           062, 366, 691, 379, 687, 842, 037, 357, 720, 742, 
                           330, 005, 039, 923, 311, 424, 242, 749, 321, 054, 
                           669, 316, 342, 299, 534, 105, 667, 488, 640, 672, 
                           576, 540, 316, 486, 721, 610, 046, 656, 447, 171, 
                           616, 464, 190, 531, 297, 321, 762, 752, 533, 175, 
                           134, 014, 381, 433, 717, 045, 111, 020, 596, 284, 
                           736, 138, 646, 411, 877, 669, 141, 919, 045, 780, 
                           407, 164, 332, 899, 165, 726, 600, 325, 498, 655, 
                           357, 752, 768, 223, 849, 647, 063, 310, 863, 251, 
                           366, 304, 282, 738, 675, 410, 389, 244, 031, 121, 
                           303, 263 }
                };

        /// <summary>
        ///     The codewords are the low-level encoding. Each codeword is 
        ///     represented by 3 characters.
        /// 
        ///     Encoding is performed in two stages:
        ///     first the data is converted to "codewords" (high-level encoding) 
        ///     and then those are converted to bars and spaces patterns 
        ///     (low-level encoding). Each codeword (CW for short) comprises 
        ///     17 modules, for a total of 4 bars and 4 spaces. Each module 
        ///     represents a bit.
        /// 
        ///     Since there are 17 modules and the first is always a bar and the 
        ///     last is always blank, the remaining 15 bits can be subdivided 
        ///     into 3 parts, 5 bits each. 5 bits yield 32 possible values. 
        ///     Each value has been encoded into a character of a special 
        ///     TrueType font.
        /// 
        ///     So for every possible CW, 3 chars are needed, plus a delimiter 
        ///     to represent the remaining 2 bits.
        /// 
        ///     Each table is used once every three rows of the bar code.
        /// </summary>
        private static readonly string[] Codewords = 
        {
             /* table 1 */
             "AxaDlyEvEAtqDjCEuFvdaAryvbqkbavaykaqvxaAByDnEvtqAzC" +
             "DmFldavrylbqlxavByADEltqvzCACFlryvyElByvDElzCvCFlDE" +
             "ExDAlqDfCEsFutaAjyDeEurqAiCjbauqyAiojaquqmjaiuBqAnC" +
             "DgFjtauzyAmEjrquyCAmpjqyuyojBquDCAoFjzyuCEjyCuCpjDC" +
             "uEFjCEjCpulaAfyDcEujqAeCDcpirauiyAeoiqquimiqiiqeiBa" +
             "unyAgEizqumCAgpiyyumoiymiygiDyuoEiCCuopiCoiEEufqAcC" +
             "DbpijaueyAcoiiquemAchiiiuegiieuedinqugCAdpimyugoimm" +
             "ughimgimduhpiohifaucyAboiequcmAbhieiucgieeucdiecigy" +
             "igmiggicqAaxubgubdicbzlqCvCEkFstazjyCuEsrqziCCupfba" +
             "sqyfaqsBqznCCwFftaszyzmEfrqsyCfqyfqmfBqsDCzoFfzysCE" +
             "fyCfyofDCsEFfCEfEFBlaDvyEAEBjqDuCEApwraBiyDuowqqBim" +
             "DuhwqiBigslazfyCsEwBasjqzeCCspwzqBmCDwpnraeqqsimzeh" +
             "nqqwymnqieBasnyzgEnBaezqsmCzgpnzqwCCBopnyyeymnymeDy" +
             "soEnDyeCCsopnCCwEpnCoeEEnEEeEpnEpBfqDsCEzpwjaBeyDso" +
             "wiqBemDshwiiBegwiewicsfqzcCCrpwnqseyzcomzaeiqBgozch" +
             "myqeiisegmyiwmgsedeicenqsgCzdpmDqemysgomCywoosghmCm" +
             "emgemdeoCshpmECeoomEoeohmEhmFpwfaBcyDroweqBcmDrhwei" +
             "BcgweeBcdwecwebefascyzbomnaeeqscmzbhmmqwgmBdhmmieee" +
             "scdmmewgdmmcegysdomoyegmsdhmomwhhmogegdmodmpomphwcq" +
             "BbmDqxwciBbgwceBbdwccwcbecqsbmzaxmgqecisbgmgiwdgsbd" +
             "mgeeccmgcecbedmmhmmhgmhdBawBatwbbsawebemdemdcmdbrla" +
             "yvyCkErjqyuCcrariyyuocqqrimcqicqecBarnyywEczqrmCywp" +
             "cyyrmocymcygcDyroEcCCropcCocEEcEpzvqCACEnptjazuyCAo" +
             "tiqzumCAhtiizugtiezudrfqysCCjptnqreyysogzaciqzwoysh" +
             "gyqtmmreggyiciegyecnqrgCytpgDqcmyzxpgCytoorghgCmcmg" +
             "gCgcoCrhpgECcoogEocohcppgFpBvaDAyEDoBuqDAmEDhBuiDAg" +
             "BueDAdBuctfazsyCzoxnateqDBoCzhxmqBwmDBhxmiteezsdxme" +
             "BwdtebcfarcyyrognaceqrcmyrhoDagmqtgmzthoCqxomBxhrcd" +
             "oCigmetgdoCecebcgyrdogoycgmrdhoEygomthhoEmxphcgdgod" +
             "chogpochhoFogphBsqDzmECxBsiDzgBseDzdBscBsbtcqzrmCyx" +
             "xgqtciDzxxgiBtgzrdxgetccxgctcbxgbccqrbmyqxggqccirbg" +
             "ooqggitdgrbdooixhgcccooeggcccbggbcdmrbxghmcdgopmghg" +
             "cddopgghdcdxopxBriDywBreDytBrcBrbtbizqwxditbezqtxde" +
             "Brtxdctbbxdbcbirawgdicberatohigdetbtohexdtcbbohcgdb" +
             "ohbgdwohwohtDylBqrzqltastarcaugbuoducargbrqvqbjaquy" +
             "biqqumykhbiiqugbiequdbnqqwCylpbmyqwobmmqwhbmgbmdboC" +
             "qxpboobohbpprvayAyCnoruqyAmruiyAgrueyAdrucbfaqsyyjo" +
             "dnabeqyBoyjhdmqrwmyBhdmibeeqsddmerwdbebbgyqtodoybgm" +
             "qthdomrxhdogbgdbhodpobhhdphzAqCDmEoxzAiCDgzAeCDdzAc" +
             "zAbrsqyzmtwqrsiyzgtwizBgyzdtwersctwcrsbtwbbcqqrmdgq" +
             "bciyzxhoqdgirtgqrdhoitxgbcchoedgcbcbdgbbdmqrxdhmbdg" +
             "hpmdhgbddhpgdhdbdxdhxhpxDDiEEwDDeEEtDDcDDbzziCCwBBi" +
             "DDwCCtBBeDDtBBczzbBBbrriyywttirreyytxxittezztxxeBBt" +
             "rrbxxcttbxxbbbiqqwddibbeqqthhidderrtppihhetttbbbppe" +
             "xxtddbppcbbwddwbbthhwddtppwhhtpptDCuEElDCsDCrzyuCCl" +
             "BzuDCBBzszyrBzrrquyyltrurqsxtutrsrqrxtstrrxtrbauqql" +
             "dburqBhdudbsbarphuhdsdbrphshdrphrdbBphBDCjByAByztqA" +
             "xrAxrzdaAhbApdApdzavaauqqkmauiaueaucawyawmawgawdaxo" +
             "axhqAqqAiyngqAeyndqAcqAbasqqjmbwqqBmqjgbwiqBgbweqBd" +
             "bwcasbbwbatmqjxbxmqBxbxgatdbxdatxbxxyDiyDeyDcyDbqzi" +
             "rBiyDwymtrBeyDtrBcqzbrBbaribtiareqitdxibtearcdxebtc" +
             "arbdxcbtbarwbtwartdxwbttdxtCEuCEsCEryCuzDuCEBzDsyCr" +
             "zDrqyuymlrzuyCBtBurzsqyrtBsrzrtBraquqilbruqyBdtubrs" +
             "aqrhxudtsbrrhxsdtraqBbrBdtBhxBEFkEFjCEkDEACEjDEzyCk" +
             "zCAyCjBDAzCzBDzqykryAqyjtzAryzxBA", 
             /* table 2 */
             "EvqFkCDjaEuyFkoDiqEumFkhDiiEugDieEudDicDnqEwCFlpAza" +
             "DmyEwoAyqDmmEwhAyiDmgAyeDmdAycADqDoCExpvzaACyDoovyq" +
             "ACmDohvyiACgvyeACdvycvDqAECDpplzavCyAEolyqvCmAEhlyi" +
             "vCglyevCdlDqvECAFplCyvEolCmvEhlCglECvFplEolEhDfaEsy" +
             "FjoDeqEsmFjhDeiEsgDeeEsdDecDebAnaDgyEtoAmqDgmEthAmi" +
             "DggAmeDgdAmcAmbuDaAoyDhouCqAomDhhuCiAoguCeAoduCcuCb" +
             "jDauEyApojCquEmAphjCiuEgjCeuEdjCcjEyuFojEmuFhjEgjEd" +
             "jFojFhDcqErmFixDciErgDceErdDccDcbAgqDdmErxAgiDdgAge" +
             "DddAgcAgbuoqAhmDdxuoiAhguoeAhduocuobiEqupmAhxiEiupg" +
             "iEeupdiEciEbiFmupxiFgiFdiFxDbiEqwDbeEqtDbcDbbAdiDbw" +
             "AdeDbtAdcAdbuhiAdwuheAdtuhcuhbipiuhwipeuhtipcipbipw" +
             "iptDauEqlDasDarAbuDaBAbsAbruduAbBudsudrihuudBihsihr" +
             "DakDajAaAAazubAubzCvaEkyFfoCuqEkmFfhCuiEkgCueEkdCuc" +
             "CubznaCwyElozmqCwmElhzmiCwgzmeCwdzmczmbsDazoyCxosCq" +
             "zomCxhsCizogsCezodsCcsCbfDasEyzpofCqsEmzphfCisEgfCe" +
             "sEdfCcfEysFofEmsFhfEgfEdfFofFhEAqFnmnpyEAiFngnhCEAe" +
             "FndndEEAcEAbCsqEjmFexDwqCsiFnxDwiEBgEjdDweCscDwcCsb" +
             "DwbzgqCtmEjxBoqzgiCtgBoiDxgCtdBoezgcBoczgbBobsoqzhm" +
             "CtxwEqsoizhgwEiBpgzhdwEesocwEcsobwEbeEqspmzhxnEqeEi" +
             "spgnEiwFgspdnEeeEcnEceEbeFmspxnFmeFgnFgeFdnFdeFxEzi" +
             "FmwmxCEzeFmtmtEEzcmrFEzbCriEiwDtiCreEitDteEztDtcCrb" +
             "DtbzdiCrwBhizdeCrtBheDttBhczdbBhbshizdwwpishezdtwpe" +
             "BhtwpcshbwpbepishwmFiepeshtmFewptmFcepbmFbepwmFwept" +
             "mFtEyuFmlmlEEysmjFEyrCquEilDruEyBDrsCqrDrrzbuCqBBdu" +
             "zbsBdszbrBdrsduzbBwhuBdBwhssdrwhrehusdBmpuehsmpsehr" +
             "mprehBmpBEykmfFEyjCqkDqACqjDqzzaABbAzazBbzsbAwdAsbz" +
             "wdzedAmhAedzmhzEyfCqfDqnzanBaDsaDwbDCkqEfmFcxCkiEfg" +
             "CkeEfdCkcCkbywqClmEfxywiClgyweCldywcywbroqyxmClxroi" +
             "yxgroeyxdrocrobcEqrpmyxxcEirpgcEerpdcEccEbcFmrpxcFg" +
             "cFdcFxEniFgwgxCEneFgtgtEEncgrFEnbCjiEewCBiCjeEetCBe" +
             "EntCBcCjbCBbytiCjwzxiyteCjtzxeCBtzxcytbzxbrhiytwtpi" +
             "rheytttpezxttpcrhbtpbcpirhwgFicperhtgFetptgFccpbgFb" +
             "cpwgFwcptgFtFouoxyxlEFosotCxjFFororEoqFEmuFglglEEDu" +
             "FoBoBEgjFEDsEmrozFEDrCiuEelCzuCisDBuEDBCirDBsCzrDBr" +
             "yruCiBztuyrsBxuztsyrrBxsztrBxrrduyrBthurdsxputhsrdr" +
             "xpsthrxprchurdBgpuchsoFugpschroFsgproFrchBgpBFokolC" +
             "xfFFojojEoiFEmkgfFECAEmjonFECzCikCyACijDzACyzDzzyqA" +
             "zrAyqzBtAzrzBtzrbAtdArbzxhAtdzxhzcdAghAcdzopAghzopz" +
             "FofofEoeFEmfECnCifCynDyDyqnzqDBrDraDtbDxdDcbDgdDohD" +
             "ocFCfiEcwCfeEctCfcCfbyliCfwyleCftylcylbqxiylwqxeylt" +
             "qxcqxbbpiqxwbpeqxtbpcbpbbpwbptEguFdldlEEgsdjFEgrCeu" +
             "EclCnuCesCnsCerCnryjuCeByBuyjsyBsyjryBrqtuyjBrxuqts" +
             "rxsqtrrxrbhuqtBdpubhsdpsbhrdprbhBdpBFhkhlCtvFFhjhjE" +
             "hiFEgkdfFEoAEgjhnFEozCekCmACejCDACmzCDzyiAyzAyizzBA" +
             "yzzzBzqrArtAqrztxArtztxzbdAdhAbdzhpAdhzhpzplyxvEpjC" +
             "xuFpiEpipFhfhfEFpnpnEheFpmFEgfEonEEDCefCmnCCDDDDyin" +
             "yyDzzDBBDqqDrrDttDxxDbbDddDhhDpfCxsFpeEpephcFpgFpcE" +
             "pcppbpCcuCcsCcryfuCcByfsyfrqluyfBqlsqlraxuqlBaxsaxr" +
             "axBEdkbvFEdjCckCgACcjCgzyeAynAyezynzqjAqBAqjzqBzatA" +
             "bxAatzbxzFdvdvEduFEdfEhnCcfCgnCoDyenymDyDDqiDqzDrBD" +
             "arDbtDdxDhvCtAFhuEhupdsFhwFpvyxAEpuCxAppuopuhhsEpwE" +
             "hsppwppsCxzppsopshhrpptpproprhpqxycAyczqfAqfzalAalz" +
             "CdnycnygDqeDqnDajDaBDbAFdAEdAphACtDphAohAhdzphBppAy" +
             "xDopAmxDhpAgpAdhzopBohzhpBhpzmxCxpzgpzdhyxpzxpywpyt" +
             "bDpdDodDhhDmtExhDghDddCxhDxhCwhCt", 
             /* table 3 */
             "vpqAxCkpavhyAtEkhqvdCArFkdyvbEkbCFlilpqvxCFlelhyvtE" +
             "FlcldCvrFFlblbEExiFlwlxCExeFltltEExclrFExbDpiExwDpe" +
             "ExtDpcDpbAFiDpwAFeDptAFcAFbvFiAFwvFeAFtvFcjpauxyAlE" +
             "jhqutCAjFjdyurEjbCuqFjaEFjujxyuBEFjsjtCuzFFjrjrEjqF" +
             "EtuFjBjBEEtsjzFEtrDhuEtBDhsDhrApuDhBApsApruFuApBuFs" +
             "uFrixqulCAfFityujEirCuiFiqEiqpFiAiBCunFFizizEiyFErA" +
             "iDFErzDdADdzAhAAhzupAupzilyufEijCueFiiEiipFininEimF" +
             "EqDDbDAdDifCucFieEiepigFicEicpfpasxyzlEfhqstCzjFfdy" +
             "srEfbCsqFfaEFfufxysBEFfsftCszFFfrfrEfqFEluFfBfBEEls" +
             "fzFElrCxuElBCxsCxrzpuCxBzpszprsFuzpBsFssFrwxqBlCDvF" +
             "nhawtyBjEndqwrCBiFnbywqEnaCwqpnaoexqslCzfFnxqetysjE" +
             "ntywzEsiFnrCeqEnqEeqpnqpFeAeBCsnFFnAFeznBCezEFnznzE" +
             "eyFnyFEjAeDFEBAEjznDFEBzCtADxACtzDxzzhABpAzhzBpzspA" +
             "spzmxawlyBfEmtqwjCBeFmrywiEmqCwipmqomqhelysfEmByejC" +
             "seFmzCwmFmyEeipmypFenenEFmDmDEemFmCFEiDEzDCrDDtDzdD" +
             "BhDshDmlqwfCBcFmjyweEmiCwepmiomihefCscFmnCeeEmmEeep" +
             "mmpegFmoFmfywcEmeCwcpmeomehecEmgEecpmgpmcCwbpmcomch" +
             "ebpmdpmbombhcxqrlCyvFctyrjEcrCriFcqEcqpFcAcBCrnFFcz" +
             "czEcyFEfAcDFEfzClAClzyxAyxzrpArpzgxatlyzvEgtqtjCzuF" +
             "grytiEgqCtipgqogqhclyrfEgBycjCreFgzCtmFgyEcipgypFcn" +
             "cnEFgDgDEcmFgCFEeDEnDCjDCBDytDzxDrhDxlqBvCDAFotaxjy" +
             "BuEorqxiCBupoqyxiooqmxihoqgglqtfCzsFoBqgjyteEozyxmE" +
             "tepoyCgiooyogihoyhcfCrcFgnCceEoDCgmEcepoCEgmpoCpcgF" +
             "goFoEFolaxfyBsEojqxeCBspoiyxeooimxehoigoidgfytcEony" +
             "geCtcpomCxgpomogehomhccEggEccpooEggpoopofqxcCBrpoey" +
             "xcooemxchoegoedgcCtbpogCgcoogogchoghcbpgdpohpocyxbo" +
             "ocmxbhocgocdgboodogbhodhobmxaxobgobdgaxobxoawoatbly" +
             "qvEbjCquFbiEbipbnEbmFEcDCfDylDqxDdlqrvCyAFdjyruEdiC" +
             "rupdiodihbfCqsFdnCbeEdmEbepdmpbgFdoFhlatvyzAEhjqtuC" +
             "zAphiytuohimtuhhighiddfyrsEhnydeCrsphmCdeohmodehhmh" +
             "bcEdgEbcphoEdgphopxvqBACDDppjaxuyBAopiqxumBAhpiixug" +
             "piexudpichfqtsCzzppnqheytsopmyxwotshpmmhegpmghedpmd" +
             "dcCrrphgCdcopoChgodchpoohghpohbbpddphhpppppfaxsyBzo" +
             "peqxsmBzhpeixsgpeexsdpecpebhcytropgyhcmtrhpgmxthpgg" +
             "hcdpgddbohdodbhphohdhphhpcqxrmByxpcixrgpcexrdpccpcb" +
             "hbmtqxpdmhbgpdghbdpdddaxhbxpdxpbixqwpbexqtpbcpbbhaw" +
             "pbwhatpbtpauxqlpasparhalpaBavCqkFauEaupawFbvyqAEbuC" +
             "qApbuobuhasEbwEaspbwpdvqrACyDpduyrAodumrAhdugdudbsC" +
             "qzpdwCbsodwobshdwharpbtpdxphvatAyzDohuqtAmzDhhuitAg" +
             "huetAdhuchubdsyrzohwydsmrzhhwmtBhhwgdsdhwdbrodtobrh" +
             "hxodthhxhxAqBDmDExxAiBDgxAeBDdxAcxAbhsqtzmzCxpwqhsi" +
             "tzgpwixBgtzdpwehscpwchsbpwbdrmryxhtmdrgpxmhtgdrdpxg" +
             "htdpxdbqxdrxhtxpxxxziBCwxzeBCtxzcxzbhritywptihretyt" +
             "ptexztptchrbptbdqwhrwdqtptwhrtpttxyuBClxysxyrhqutyl" +
             "pruhqsprshqrprrdqlhqBprBxykxyjhqkpqAhqjpqzakEakpaAC" +
             "qnpaAoaAhajpaBpbAyqDobAmqDhbAgbAdazobBoazhbBhdAqrDm" +
             "yExdAirDgdAerDddAcdAbbzmqCxdBmrDxdBgbzddBdayxbzxdBx" +
             "tDizEwtDezEttDctDbdzirCwhBitDwrCthBedzchBcdzbhBbbyw" +
             "dzwbythBwdzthBtBEuDFlBEsBErtCuzElxDutCsxDstCrxDrdyu" +
             "rClhzudyspBuhzsdyrpBshzrpBrbyldyBhzBpBBBEkBEjtCkxCA" +
             "tCjxCzdykhyAdyjpzAhyzpzzBEftCfxCndyfhynpyDanoanhaDm" +
             "qoxaDgaDdamxaDxbDiqEwbDeqEtbDcbDbaCwbDwaCtbDtrEuyFl" +
             "rEsrErbCuqEldDurEBdDsbCrdDraClbCBdDBzFkzFjrEktEArEj" +
             "tEzbCkdCAbCjhDAdCzhDzzFfrEftEnbCfdCnhCDaowaotaEuqpl" +
             "aEsaEraolaEBqFkqFjaEkbEAaEjbEzyFv"
         };

        /// <summary>
        ///     Represents a line break
        /// </summary>
        private const string Break = "\n";

        /// <summary>
        ///     Represents the last bit of a CW and the first bit of the next CW
        ///     It is defined as the '*' character on the special font
        /// </summary>
        private const string SepCol = "*";

        /// <summary>
        ///     Represents the special CW that marks the beginning of every row 
        ///     of the bar code as defined by the PDF417 spec. It is defined as 
        ///     the '+' character on the special font. Note that a separator is 
        ///     necessary to represent the last bit of the CW and the first bit 
        ///     of the following CW.
        /// </summary>
        private const string BeginRow = "+" + SepCol;

        /// <summary>
        ///     Represents the special CW that marks the end of every row of 
        ///     the bar code as defined by the PDF417 spec. It is defined as the 
        ///     '-' character on the special font. Note that a separator is 
        ///     necessary to represent the last bit of the previous CW and the 
        ///     first bit of the ending CW.
        /// </summary>
        private const string EndRow = SepCol + "-";

        #endregion

        #region Private methods

        /// <summary>
        ///     Gets the lowest table from a bitwise integer representing all 
        ///     tables of a given character.
        /// </summary>
        /// <param name="bitTables">
        ///     Bitwise integer containing the tables for a character
        /// </param>
        /// <returns>Table number. Either 1, 2, 4 or 8</returns>
        /// <revision revisor="dev13" date="11/18/2009" versoin="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static int GetLowestTable(int bitTables)
        {
            if ((bitTables & 1) == 1)
            {
                // The first bit is set, so return that
                return 1;
            }
            else if ((bitTables & 2) == 2)
            {
                // The lowest set bit is the second
                return 2;
            }
            else if ((bitTables & 4) == 4)
            {
                // The lowest set bit is the third
                return 4;
            }
            else
            {
                // The lowest set bit is the fourth. Note that since these 
                // values have been predefined (see Pdf417.Characters) no other 
                // values are expected.
                return 8;
            }
        }

        /// <summary>
        ///     Appends the codes to switch from one character table to another
        /// </summary>
        /// <param name="lstCode">List of character codes</param>
        /// <param name="tabCur">Current table number</param>
        /// <param name="bitThis">
        ///     Bitwise integer for all the tables for current character.
        /// </param>
        /// <param name="bitNext">
        ///     Bitwise integer for all the tables for the next character.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the switch is only for the current character, or
        ///     <c>false</c> if the switch is permanent for the next 2 or 
        ///     more characters.
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" versoin="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static bool AppendTableSwitch(
            List<int> lstCode, int tabCur, int bitThis, int bitNext)
        {
            bool onlyForNextCharacter = false,

                 // if the current table is the same of two chars ahead, then
                 // a temporary switch is required, although only two switches 
                 // of this kind are available.
                 singleSwitchNeeded = (bitThis & bitNext) == 0;

            // get the lowest table of the current character
            int tabThis = GetLowestTable(bitThis);

            // if a temporary switch is needed, see if it is one of the 
            // two available 
            if ((singleSwitchNeeded == true)
                && ((tabCur != 8) && (tabThis == 8)))
            {
                // current table is either 1, 2 or 4, and only for the next
                // character, table 8 is required
                lstCode.Add(29);
                onlyForNextCharacter = true;
            }
            else if ((singleSwitchNeeded == true)
                     && ((tabCur == 2) && (tabThis == 1)))
            {
                // current table is 2 and only for the next character, table 1
                // is required
                lstCode.Add(27);
                onlyForNextCharacter = true;
            }
            else
            {
                // change table permanently for next characters
                // to simplify the code, combine both table numbers in one 
                // single integer by left shifting tabThis and adding to tabCur 
                // tabThis -> 1111 0000 [1, 2, 4 or 8] << 4
                // tabCur  -> 0000 1111 [1, 2, 4 or 8]
                tabThis = (tabCur << 4) + tabThis;
                switch (tabThis)
                {
                    // 0001 0010, (1 -> 2)
                    case 18:

                    // 0100 0010, (4 -> 2)
                    case 66:
                        lstCode.Add(27);
                        break;

                    // 0001 1000, (1 -> 8)
                    case 24:

                    // 0010 1000, (2 -> 8)
                    case 40:
                        lstCode.Add(28);
                        lstCode.Add(25);
                        break;

                    // 0010 0001, (2 -> 1)
                    case 33:
                        lstCode.Add(28);
                        lstCode.Add(28);
                        break;

                    // 0100 1000, (4 -> 8)
                    case 72:
                        lstCode.Add(25);
                        break;

                    // 1000 0001, (8 -> 1)
                    case 129:
                        lstCode.Add(29);
                        break;

                    // 1000 0010, (8 -> 2)
                    case 130:
                        lstCode.Add(29);
                        lstCode.Add(27);
                        break;

                    // 1000 0100, (8 -> 4)
                    case 132:
                        lstCode.Add(29);
                        lstCode.Add(28);
                        break;

                    // (1 -> 4), (2 -> 4) or (4 -> 1)
                    default:
                        lstCode.Add(28);
                        break;
                }
            }

            return onlyForNextCharacter;
        }

        /// <summary>
        ///     Make sure that error level will not create enough CWs to make 
        ///     the bar code having more than 928 which is the limit by 
        ///     definition. If that happens the security level will be reduced 
        ///     in an attepmt to have 928 CWs or less. If that is not possible, 
        ///     -1 is returned.
        /// </summary>
        /// <param name="level">an error level by default (0 to 8)</param>
        /// <param name="cwData">data codewords</param>
        /// <param name="dataCols">
        ///     number of data columns to have on the bar code
        /// </param>
        /// <returns>
        ///     -1: the data is too long. No bar code is possible
        ///     0-8: error level
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static int CheckErrorLevel(
            int level, List<int> cwData, int dataCols)
        {
            double dblLen = 0;
            int intLen;

            // make sure the error level is in the valid range 0 to 8
            if (level < 0)
            {
                level = 0;
            }
            else if (level > 8)
            {
                level = 8;
            }

            // make sure that error level will not make the bar code to have
            // more than 928 codewords
            do
            {
                // get the length of total CWs (including data and security) the
                // bar code will have
                dblLen = Convert.ToDouble(cwData.Count) + 1D
                         + Math.Pow(2, level + 1);

                // The bar code needs to have CWs on every column and every row.
                // Depending on the number of columns, some additional CWs might
                // be necessary for padding.
                dblLen = Math.Ceiling(dblLen / dataCols) * dataCols;
                intLen = Convert.ToInt32(dblLen);
                if (intLen > 928)
                {
                    // try using the next lower security level
                    level = level - 1;
                }
            }

             // if security level is -1 abort the search (the data is too long)
             // if the total number of CWs is 928 or less, stop the search and
             // return the current security level since it is Ok.
            while ((level > -1) && (intLen > 928));
            return level;
        }

        /// <summary>
        ///     Gets the recommended security level according to the PDF417 
        ///     specs. Note that for practical purposes, a lower level might 
        ///     yield a valid bar code, only not as resistant.
        /// </summary>
        /// <param name="cwData">data codewords</param>
        /// <param name="dataCols">number of data columns</param>
        /// <returns>Returns 2, 3, 4 or 5</returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static int GetErrorLevel(List<int> cwData, int dataCols)
        {
            // Depending on the number of data CWs, there is a recommended value
            // for seurity level in order to produce a bar code resistant to 
            int length = cwData.Count,
                level = 0;

            // for levels[i] characters or less use i as the recommended error
            // level
            int[] levels = { 0, 2, 6, 14, 30, 62, 126, 254, 510 };
            while ((level < levels.Length) && (length <= levels[level]))
            {
                // increment level by 1
                level = level + 1;
            }

            // note that error level might be up to 8
            return level;
        }

        /// <summary>
        ///     Returns the mode based on the character code. Note that it is 
        ///     assumed that range is 0-255, Unicode is not currently supported.
        /// </summary>
        /// <param name="character">a character from the data string</param>
        /// <returns>
        ///     900 (text mode)
        ///     901 (binary mode)
        ///     902 (numeric mode)
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static int GetCharMode(char character)
        {
            int val = ((int) character), mode;
            if ((48 <= val) && (val <= 57))
            {
                // numeric mode
                mode = 902;
            }
            else if ((val == 9)
                     || (val == 10)
                     || (val == 13)
                     || ((32 <= val) && (val <= 126)))
            {
                // text mode
                mode = 900;
            }
            else
            {
                // binary mode
                mode = 901;
            }

            return mode;
        }

        /// <summary>
        ///     Based on the character range, this method splits the data into 
        ///     blocks that are encoded differently. e.g.let data = 
        ///     Abcdefg12345678 
        ///     block 0 = [mode 900, 7 chars]
        ///     block 1 = [mode 902, 8 chars] 
        ///     Note that after splitting the data into separate blocks, 
        ///     additional optimizations are performed that could merge 2 or 
        ///     more blocks together.
        /// </summary>
        /// <param name="data">the entire data to be encoded</param>
        /// <returns>
        ///     A list of [Block], this object describes each block by 2 
        ///     properties, mode and # of characters.
        /// </returns>
        private static List<Block> GetEncodingBlocks(string data)
        {
            // a list of all blocks found
            List<Block> blocks = new List<Block>();

            Block block = null;

            // initialize current mode as -1 so that the first character 
            // triggers the creation of a new block
            int mode, curMode = -1;

            for (int i = 0, len = data.Length; i < len; i++)
            {
                // select mode for current character
                mode = GetCharMode(data[i]);

                // if the mode is the same, continue
                if (mode != curMode)
                {
                    // if the mode is different, create a new block
                    block = new Block(mode);
                    blocks.Add(block);

                    // make the new mode the current mode
                    curMode = mode;
                }
                else
                {
                    block.Count = block.Count + 1;
                }
            }

            return blocks;
        }
            
        /// <summary>
        ///     Merges 2 blocks together by combining the characters of both and 
        ///     taking the mode of one of them
        /// </summary>
        /// <param name="blocks">the list of all blocks</param>
        /// <param name="source">
        ///     source block. the mode of this block is discarded
        ///     after combining with the destination block
        /// </param>
        /// <param name="dest">
        ///     destination block. this block's mode is the prevailing mode of 
        ///     the combined block
        /// </param>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static void MergeBlocks(
            List<Block> blocks, int source, int dest)
        {
            // get source and destination blocks
            Block srcBlock = blocks[source],
                  dstBlock = blocks[dest];

            // increment the amount of character from the source block. Mode
            // remains intact
            dstBlock.Count = dstBlock.Count + srcBlock.Count;

            // remove the source block from the list
            blocks.Remove(srcBlock);
        }

        /// <summary>
        ///     After all blocks have been created based on solely the character
        ///     value, this method merges blocks together if by doing so, the 
        ///     resulting amount of CWs is reduced. This is possible since 
        ///     encoding modes are not mutualy exclusive and characters can be 
        ///     encoded in more than just one mode.
        /// </summary>
        /// <param name="blocks">the list of all blocks</param>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static void OptimizeNumericEncoding(List<Block> blocks)
        {
            // It is assumed that checking if there are more than just 1 block
            // has been done before calling this method
            // block is the numeric block
            // block2 is the other block we are comparing to (either the 
            // previous or the next one).
            Block block = blocks[0], 
                  block2 = blocks[1],
                  block1;
            block = blocks[0];

            // Verify the first block
            if (block.Mode == 902)
            {
                // first block is NUMERIC (mode 902)
                if ((block.Count < 8) && (block2.Mode == 900))
                {
                    // less than 8 digits, followed by text block
                    MergeBlocks(blocks, 0, 1);
                }
                else if ((block.Count == 1) && (block2.Mode == 901))
                {
                    // 1 digit followed by binary block
                    MergeBlocks(blocks, 0, 1);
                }
            }

            for (int i = 1; i < (blocks.Count - 1); i++)
            {
                block = blocks[i];
                if (block.Mode == 902)
                {
                    // block is NUMERIC (mode 902), so take preceding and 
                    // following blocks
                    block1 = blocks[i - 1];
                    block2 = blocks[i + 1];
                    if ((block1.Mode == 901) && (block.Count < 4) && 
                        (block2.Mode == 901))
                    {
                        // less than 4 digits, between binary blocks. So merge
                        // all 3 blocks together as a single binary block
                        MergeBlocks(blocks, i, i + 1);
                        MergeBlocks(blocks, i - 1, i);
                    }
                    else if (block1.Mode == 900)
                    {
                        // previous mode is TEXT (mode 900)
                        if ((block.Count < 5) && (block2.Mode == 901))
                        {
                            // less than 5 digits, preceded by text block and
                            // followed by binary block. So merge the binary
                            // and the preceding block together
                            MergeBlocks(blocks, i, i - 1);
                        }
                        else if ((block.Count < 8) && (block2.Mode == 900))
                        {
                            // less than 8 digits between text blocks. So merge
                            // all 3 blocks together as a single text block
                            MergeBlocks(blocks, i, i + 1);
                            MergeBlocks(blocks, i - 1, i);
                        }
                    }
                }
            }

            if (block2.Mode == 902)
            {
                // last block is numeric
                if ((block2.Count < 7) && (block.Mode == 900))
                {
                    // less than 7 digits, preceded by a text block
                    MergeBlocks(blocks, blocks.Count - 1, blocks.Count - 2);
                }
                else if ((block2.Count == 1) && (block.Mode == 901))
                {
                    // 1 digit preceded by a binary block
                    MergeBlocks(blocks, blocks.Count - 1, blocks.Count - 2);
                }
            }
        }

        /// <summary>
        ///     After all blocks have been created based on solely the character
        ///     value, this method merges blocks together if by doing so, the 
        ///     resulting amount of CWs is reduced. This is possible since 
        ///     encoding modes are not mutualy exclusive and characters can be 
        ///     encoded in more than just one mode.
        /// </summary>
        /// <param name="blocks">the list of all blocks</param>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static void OptimizeTextEncoding(List<Block> blocks)
        {
            Block block, block2, block1;

            // check all blocks except for the first and last
            for (int i = 1; i < blocks.Count - 1; i = i + 1)
            {
                block = blocks[i];
                if (block.Mode == 900)
                {
                    // block is in TEXT mode (900), so take the preceding and
                    // following blocks as well
                    block1 = blocks[i - 1];
                    block2 = blocks[i + 1];

                    if (block1.Mode == 901)
                    {
                        // preceding block is in binary mode
                        if ((block.Count < 5) && (block2.Mode == 901))
                        {
                            // block has less than 5 characters and following 
                            // block is also in binary mode, so merge them all 3 
                            // blocks as binary
                            MergeBlocks(blocks, i, i + 1);
                            MergeBlocks(blocks, i - 1, i);
                        }
                        else if ((block.Count < 3) && (block2.Mode != 901))
                        {
                            // block has less than 3 characters and following 
                            // block is either numeric or text, so merge this 
                            // and the preceding block together
                            MergeBlocks(blocks, i, i - 1);
                        }
                    }
                    else if ((block.Count < 3) && (block2.Mode == 901))
                    {
                        // current block has less than 3 characters and 
                        // following block is binary so merge this and the 
                        // following block together as a binary block
                        MergeBlocks(blocks, i, i + 1);
                    }
                }
            }

            // take the last two blocks
            block = blocks[blocks.Count - 2];
            block2 = blocks[blocks.Count - 1];

            if ((block2.Mode == 900) && (block2.Count == 1) &&
                (block.Mode == 901))
            {
                // last block is text, last block has only 1 character and 
                // preceding block is binary, so merge them togetether as a 
                // single binary block
                MergeBlocks(blocks, blocks.Count - 1, blocks.Count - 2);
            }
        }

        /// <summary>
        ///     Optimizes encoding by reducing blocks that lead to a reduced 
        ///     amount of CWs on the bar code.
        /// </summary>
        /// <param name="data">
        ///     The entire string of data to be encoded on the bar code
        /// </param>
        /// <returns>the optimized list of blocks</returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static List<Block> OptimizeEncoding(string data)
        {
            // get the preliminary list of blocks based solely on 
            // character values
            List<Block> blocks = GetEncodingBlocks(data);

            // optimize blocks by merging adjacent blocks that result in a 
            // reduced amount of CWs
            if (blocks.Count > 1)
            {
                // optimize numeric encoding
                OptimizeNumericEncoding(blocks);
            }

            if (blocks.Count > 1)
            {
                // optimize text encoding
                OptimizeTextEncoding(blocks);
            }

            return blocks;
        }

        /// <summary>
        ///     Since the bar code is 2D, the amount of CWs encoded (including 
        ///     data and error correction) might yield an incomplete bar, by not 
        ///     providing enough CWs for all rows. This method adds padding CWs 
        ///     (will not alter the encoded data) as necessary to prevent that.
        /// </summary>
        /// <param name="cwData">all data CWs</param>
        /// <param name="errorLevel">error level [0..8]</param>
        /// <param name="dataCols">columns [1..30]</param>
        /// <returns>Number of rows of bar code</returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static Pad CheckDataLength(
            List<int> cwData, int errorLevel, int dataCols)
        {
            int intLen, cwInLastRow, cwPadding = 0;

            // [length CW] + [data CWs] + [error CWs]
            intLen = 1 + cwData.Count
                     + Convert.ToInt32(Math.Pow(2, errorLevel + 1));

            // PDF417 needs at least 3 rows
            cwPadding = 3 * dataCols - intLen;

            if (cwPadding < 0)
            {
                // a negative cwPadding means that there are more CW than the
                // minimum required to have 3 rows. So make sure that all rows 
                // are complete
                cwInLastRow = intLen % dataCols;

                if (cwInLastRow > 0)
                {
                    // some blank spaces are left on the last row, so additional
                    // CWs need to be added
                    cwPadding = dataCols - cwInLastRow;
                }
                else
                {
                    // last row is complete so nothing needs to be added
                    cwPadding = 0;
                }
            }

            // append the padding CWs if necessary
            for (int i = 0; i < cwPadding; i = i + 1)
            {
                // padding CWs are a simple TEXT mode switch
                cwData.Add(900);
            }

            // insert at the beginning the length CW. This length includes the
            // descriptor itself
            cwData.Insert(0, cwData.Count + 1);

            // number of rows = ((real CWs) + (padding CWs)) / (number 
            // of columns)
            Pad pad;
            pad.Padding = cwPadding;
            pad.Rows = ((intLen + cwPadding) / dataCols);
            return pad;
        }

        /// <summary>
        ///     Encodes a block of characters in BINARY mode
        /// </summary>
        /// <param name="block">
        ///     substring that is to be encoded as binary
        /// </param>
        /// <returns>a list of high level encoded CWs</returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static List<int> EncodeAsBinary(string block)
        {
            List<int> lstCw = new List<int>();

            // every 6 chars are encoded as 5 codewords
            int[] intCw = new int[5];
            int len = block.Length, idx = 0;
            long t = 0;

            if (len == 1)
            {
                // switch to binary only for the next codeword
                lstCw.Add(913);

                // since Unicode is not supported, Unicode characters are 
                // catalogued as BINARY. using a bitwise AND operation, only the 
                // least significant byte is encoded, rather than ignoring the 
                // whole character altogether
                lstCw.Add(255 & block[0]);
            }
            else
            {
                // depending on the length of the block, PDF417 standard 
                // requires a different switch
                if (len % 6 == 0)
                {
                    // switch to binary mode (codewords are multiple of 6)
                    lstCw.Add(924);
                }
                else
                {
                    // switch to binary mode
                    lstCw.Add(901);
                }

                // encode in chuncks of 6 chars at a time. The remaining chars 
                // are encoded separately
                while (idx + 5 < len)
                {
                    // get the polynomial value as required by PDF417
                    for (int i = 0; i < 6; i = i + 1)
                    {
                        // since Unicode is not supported, Unicode characters 
                        // are catalogued as BINARY. using a bitwise AND 
                        // operation, only the least significant byte is 
                        // encoded, rather than ignoring the whole character
                        // altogether
                        t = t + ((long)(255 & block[idx + i])) *
                                Convert.ToInt64(Math.Pow(256, 5 - i));
                    }

                    // compute the codewords
                    for (int i = 0; i < 5; i = i + 1)
                    {
                        intCw[i] = (int)(t % 900);
                        t = t / 900;
                    }

                    // add the codewords in reverse order
                    for (int i = 4; i >= 0; i = i - 1)
                    {
                        lstCw.Add(intCw[i]);
                    }

                    // increment the index by 6 since 6 chars were processed
                    idx = idx + 6;
                }

                // the remaining chars are encoded using the ASCII values
                while (idx < len)
                {
                    lstCw.Add((int) block[idx]);
                    idx = idx + 1;
                }
            }

            return lstCw;
        }

        /// <summary>
        ///     Encodes a block of characters in NUMERIC mode
        /// </summary>
        /// <param name="block">block of characters</param>
        /// <returns>the list of high level encoded CWs</returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static List<int> EncodeAsNumeric(string block)
        {
            List<int> lstCw = new List<int>();

            // every 44 chars are encoded as 15 codewords
            int[] intCw = new int[15];
            int len = block.Length, idx = 0, digits = 0, cwords;

            // Since CW calculations for this encoding generates very big 
            // integers and unfortunately .NET 3.5 do not provide a native class 
            // to handle such numbers, we borrow the class BigInteger from Java 
            // for these operations.
            // t is a temporary variable
            // div is simply 900, but it needs to be created as a BigInteger 
            // type in order to perform all arithmetic operations with t
            BigInteger t, div = new BigInteger("900");

            // switch to numeric mode
            lstCw.Add(902);

            while (idx < len)
            {
                // get the number of digits to encode at once
                digits = Math.Min(44, len - idx);

                // get the number of CWs that will be produced
                cwords = (digits / 3) + 1;

                // a '1' is always added at the beginning as part of the
                // encoding algorithm defined by the PDF417 spec
                t = new BigInteger("1" + block.Substring(idx, digits));

                // compute the codewords
                for (int i = 0; i < cwords; i = i + 1)
                {
                    // get the remainder of t/div as an integer
                    intCw[i] = (t.mod(div)).intValue();

                    // save the actual result from dividing t/div
                    t = t.divide(div);
                }

                // add the codewords in reverse order
                for (int i = cwords - 1; i >= 0; i = i - 1)
                {
                    lstCw.Add(intCw[i]);
                }

                // increment the index by 44 since 44 chars were processed
                idx = idx + digits;
            }

            return lstCw;
        }

        /// <summary>
        ///     Encodes block as text
        /// </summary>
        /// <param name="block">The block to encode</param>
        /// <returns>
        ///     A list of codewords
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static List<int> EncodeAsText(string block)
        {
            List<int> lstCw = new List<int>(),
                      lstCode = new List<int>();
            int
                
                // current table (PDF417 tables, not an actual table in this 
                // code) these tables are numbered 1, 2, 4, 8. Hence the use of 
                // a bitwise integer (bitThis) for all tables of a character
                tabCur = 1,

                // bitwise number referring to the tables where a character is
                // found
                bitThis = 0, bitNext = 0,
                row = 0, rowNext = 0,
                len = block.Length, idx = 0;

            bool onlyForNextCharacter;

            // first, get the sequence codes depending on the PDF417 tables for
            // characters (See Pdf417.Characters). This sequence will include
            // corresponding char codes and switch codes 
            while (idx < len)
            {
                // the reason for subtracting 9 is because [Pdf417.Characters]
                // has 1 entry for each ASCII value in the range 9 to 127
                row = ((int) block[idx]) - 9;
                bitThis = Pdf417.Characters[row, 0];

                if ((tabCur & bitThis) != tabCur)
                {
                    // the character is in table other than the current so a 
                    // table switch is needed
                    if (idx + 1 < len)
                    {
                        // at least another character is available, so use it in
                        // order to determine if a temp switch is available
                        rowNext = ((int) block[idx + 1]) - 9;
                        bitNext = Pdf417.Characters[rowNext, 0];
                    }
                    else
                    {
                        // since this is the last character, assume a temp 
                        // switch if possible
                        bitNext = tabCur;
                    }

                    onlyForNextCharacter =
                        AppendTableSwitch(lstCode, tabCur, bitThis, bitNext);

                    if (onlyForNextCharacter == false)
                    {
                        tabCur = GetLowestTable(bitThis);
                    }
                }

                lstCode.Add(Pdf417.Characters[row, 1]);
                idx = idx + 1;
            }

            if (lstCode.Count % 2 != 0)
            {
                // since codewords are built off of pairs of codes, add a 
                // padding code if needed for the last pair. Note that regarless 
                // of the current table, code 29 is not a character, but a 
                // switch. That's the reason for adding that code.
                lstCode.Add(29);
            }

            // finally encode character codes, 2 at a time, in order to get
            // the final codewords. Reuse [idx] and [len] variables
            idx = 0;
            len = lstCode.Count;

            while (idx < len)
            {
                // build the codewords using the formula
                // CWi = (30 * code1) + code2
                lstCw.Add(30 * lstCode[idx] + lstCode[idx + 1]);
                idx = idx + 2;
            }

            return lstCw;
        }

        /// <summary>
        ///     Encodes the row left and right indicators
        /// </summary>
        /// <param name="rows"># of rows of the bar code [3..90]</param>
        /// <param name="level">error correction level</param>
        /// <param name="columns">data columns of the bar code [1..30]</param>
        /// <returns>
        ///     The encoded indicators
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static int[,] EncodeIndicators(int rows, int level, int columns)
        {
            // [left row 0 indicator, right row 0 indicator
            //  left row 1 indicator, right row 1 indicator
            //  ...]
            int[,] cwIndic = new int[rows, 2];

            // i: row index
            // j: is the remainder of i/3. This is needed since the formulas
            // needed to compute the indicators are the same every 3 rows
            int i = 0, j = 0,

                // rows-division temp
                trd = (rows - 1) / 3,

                // rows-remainder temp
                trr = (rows - 1) % 3,

                // columns-related temp
                tc = columns - 1,

                // level-related temp
                tl = level * 3,

                // i-related temp
                ti;

            while (i < rows)
            {
                j = i % 3;

                ti = 30 * (i / 3);
                switch (j)
                {
                    case 0:

                        // left indicator
                        cwIndic[i, 0] = ti + trd;

                        // right indicator
                        cwIndic[i, 1] = ti + tc;
                        break;
                    case 1:

                        // left indicator
                        cwIndic[i, 0] = ti + tl + trr;

                        // right indicator
                        cwIndic[i, 1] = ti + trd;
                        break;

                    // 2
                    default:

                        // left indicator
                        cwIndic[i, 0] = ti + tc;

                        // right indicator
                        cwIndic[i, 1] = ti + tl + trr;
                        break;
                }

                i = i + 1;
            }

            return cwIndic;
        }

        /// <summary>
        ///     Encodes the error correction CWs
        /// </summary>
        /// <param name="level">level of security</param>
        /// <param name="cwData">
        ///     list of encoded CWs of the data that goes on the bar code
        /// </param>
        /// <returns>
        ///     a list of CWs representing the error detection and correction
        ///     of the bar code
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static List<int> EncodeErrorCodewords(
            int level, List<int> cwData)
        {
            // the list that will collect the error correction CWs
            List<int> cwList = new List<int>();

            // based on the level, get the amount of CWs that will be necessary
            int cwErrLen = (int) Math.Pow(2, level + 1),
                cwDatLen = cwData.Count,

                // temporary variables used to reduce the complexity of the
                // code
                t1 = 0, t2 = 0, t3 = 0;

            // a temporary array is used to calculate the error correction CWs
            int[] cwError = new int[cwErrLen],

                  // based on the correction level, get the coefficients table
                  coef = Pdf417.Coefficients[level];

            for (int i = 0; i < cwDatLen; i++)
            {
                // Since all coefficients calculation of the polynomial 
                // equations defined by the Reed Salomon has been hardcoded (see 
                // Pdf417.Coefficients) the only remaing part of the process is
                // to apply the MOD 929 to the coefficients
                t1 = (cwData[i] + cwError[cwErrLen - 1]) % 929;

                for (int j = cwErrLen - 1; j > 0; j--)
                {
                    t2 = (t1 * coef[j]) % 929;
                    t3 = 929 - t2;
                    cwError[j] = (cwError[j - 1] + t3) % 929;
                }

                t2 = (t1 * coef[0]) % 929;
                t3 = 929 - t2;
                cwError[0] = t3 % 929;
            }

            for (int i = 0; i < cwErrLen; i++)
            {
                // After the preliminary CW calculation, get the complement of
                // the obtained values to get the final CWs and add them to the
                // list
                if (cwError[i] != 0) cwError[i] = 929 - cwError[i];
                cwList.Add(cwError[i]);
            }

            // error CW are added to the bar code in reverse order
            cwList.Reverse();

            return cwList;
        }

        /// <summary>
        ///     Encodes the data that goes on the bar code
        /// </summary>
        /// <param name="data">data to encode</param>
        /// <returns>
        ///     A list of CW that represent the high level encoding of the
        ///     data that goes on the bar code
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static List<int> EncodeDataCodewords(string data)
        {
            // this is the list that will contain all CWs that represent the 
            // data on the bar code
            List<int> cwData = new List<int>();

            // in order to encode the data in an efficient way, it is split in
            // blocks containing data of similar characteristics that require
            // one of the 3 possible high level encodings
            List<Block> blocks = OptimizeEncoding(data);
            string strBlock;

            // this index gives the position of the first character of the 
            // current block
            int idx = 0;

            foreach (Block block in blocks)
            {
                // extract the block from the data string
                strBlock = data.Substring(idx, block.Count);
                switch (block.Mode)
                {
                    case 901:

                        // the block should be encoded as binary data
                        cwData.AddRange(EncodeAsBinary(strBlock));
                        break;
                    case 902:

                        // the block should be encoded using the 
                        // numeric encoding
                        cwData.AddRange(EncodeAsNumeric(strBlock));
                        break;
                    default:

                        // by default, assume a text encoding
                        if (idx > 0)
                        {
                            // add the switch only if not the first block, since
                            // text mode is the mode by default
                            cwData.Add(900);
                        }

                        cwData.AddRange(EncodeAsText(strBlock));
                        break;
                }

                // advance the index to the beginning of the next block
                idx = idx + block.Count;
            }

            return cwData;
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Returns a character (a-zA-F, *, +, -) representation of the bar 
        ///     code. This method uses the recommended error correction level 
        ///     based on the data length.
        /// </summary>
        /// <param name="data">data to encode</param>
        /// <param name="dataCols">
        ///     Number of columns (for data only) of the bar code.
        /// </param>
        /// <returns>
        ///     Lines of text that are supposed to be represented using the 
        ///     special PDF417 font
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static Pdf417Result Encode(string data, int dataCols)
        {
            return Encode(data, dataCols, -1);
        }

        /// <summary>
        ///     Returns a character (a-zA-F, *, +, -) representation of the bar 
        ///     code. This method requires a user-defined error 
        ///     correction level.
        /// </summary>
        /// <param name="data">data to encode</param>
        /// <param name="dataCols">
        ///     Number of columns (for data only) of the bar code.
        /// </param>
        /// <param name="errorLevel">
        ///     Error detection and correction level.
        /// </param>
        /// <returns>
        ///     Lines of text that are supposed to be represented using the 
        ///     special PDF417 font.
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static Pdf417Result Encode(
            string data, int dataCols, int errorLevel)
        {
            List<int> cwData = EncodeDataCodewords(data), cwError;
            string barCode = string.Empty, table;
            int intRows, cwIdx = 0;
            int[,] cwRows;
            if ((errorLevel < 0) || (8 < errorLevel))
            {
                // get the recommended error level if the error level is outside
                // the accepted range [0..8]
                errorLevel = GetErrorLevel(cwData, dataCols);
            }

            // check the error level and adjust it if necessary so that the 
            // total number of CWs is not larger than 928
            errorLevel = CheckErrorLevel(errorLevel, cwData, dataCols);

            // check data CWs count in order to add CWs if necessary to fill the
            // bar code completely
            Pad pad = CheckDataLength(cwData, errorLevel, dataCols);
            intRows = pad.Rows;
            cwError = EncodeErrorCodewords(errorLevel, cwData);
            cwRows = EncodeIndicators(intRows, errorLevel, dataCols);

            // append error CWs to the data CWs
            cwData.AddRange(cwError);

            for (int i = 0; i < intRows; i = i + 1)
            {
                if (i > 0)
                {
                    // starting from the second row, add a line break before 
                    // appending the row text
                    barCode = barCode + Break;
                }

                // get the character table
                table = Pdf417.Codewords[i % 3];

                // add the beginning of the new row and the row left indicator
                barCode = barCode +
                          BeginRow + table.Substring(3 * cwRows[i, 0], 3)
                          + SepCol;

                for (int j = 0; j < dataCols; j = j + 1)
                {
                    // get the index of the character representation of the CW
                    // on the table of characters
                    cwIdx = 3 * cwData[i * dataCols + j];

                    // append the CW and the separator
                    barCode = barCode + table.Substring(cwIdx, 3) + SepCol;
                }

                // append the row right indicator and the end of the row
                barCode = barCode + table.Substring(3 * cwRows[i, 1], 3)
                          + EndRow;
            }

            Pdf417Result result = new Pdf417Result(
                barCode,
                dataCols,
                intRows,
                pad.Padding);
            return result;
        }

        /// <summary>
        ///     Returns the number of rows of the bar code from the raw encoded 
        ///     string.
        /// </summary>
        /// <param name="encoded">
        ///     Encoded bar code as returned from one of the two methods 
        ///     Encode(..).
        /// </param>
        /// <returns>number of rows of the bar code</returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static int GetBarCodeRows(string encoded)
        {
            // create a regular expression to find all line breaks
            Regex reg = new Regex(Break, RegexOptions.Multiline);

            // since all but the last row have line breaks, add 1
            int count = 1 + reg.Matches(encoded).Count;

            return count;
        }

        #endregion
    }
}