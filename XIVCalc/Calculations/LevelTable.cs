using System.Collections.ObjectModel;
// ReSharper disable InconsistentNaming

namespace XIVCalc.Calculations;
/// <summary>
/// Static Values that are tied to job level
/// </summary>
public static class LevelTable
{
    private static readonly ReadOnlyDictionary<int, (int MP, int MAIN, int SUB, int DIV, int HP, int ELMT, int THREAT)> _levelTable = 
        new(new Dictionary<int, (int MP, int MAIN, int SUB, int DIV, int HP, int ELMT, int THREAT)>
    {
        [1] = (10000, 20, 56, 56, 86, 52, 2),
        [2] = (10000, 21, 57, 57, 101, 54, 2),
        [3] = (10000, 22, 60, 60, 109, 56, 3),
        [4] = (10000, 24, 62, 62, 116, 58, 3),
        [5] = (10000, 26, 65, 65, 123, 60, 3),
        [6] = (10000, 27, 68, 68, 131, 62, 3),
        [7] = (10000, 29, 70, 70, 138, 64, 4),
        [8] = (10000, 31, 73, 73, 145, 66, 4),
        [9] = (10000, 33, 76, 76, 153, 68, 4),
        [10] = (10000, 35, 78, 78, 160, 70, 5),
        [11] = (10000, 36, 82, 82, 174, 73, 5),
        [12] = (10000, 38, 85, 85, 188, 75, 5),
        [13] = (10000, 41, 89, 89, 202, 78, 6),
        [14] = (10000, 44, 93, 93, 216, 81, 6),
        [15] = (10000, 46, 96, 96, 230, 84, 7),
        [16] = (10000, 49, 100, 100, 244, 86, 7),
        [17] = (10000, 52, 104, 104, 258, 89, 8),
        [18] = (10000, 54, 109, 109, 272, 93, 9),
        [19] = (10000, 57, 113, 113, 286, 95, 9),
        [20] = (10000, 60, 116, 116, 300, 98, 10),
        [21] = (10000, 63, 122, 122, 333, 102, 10),
        [22] = (10000, 67, 127, 127, 366, 105, 11),
        [23] = (10000, 71, 133, 133, 399, 109, 12),
        [24] = (10000, 74, 138, 138, 432, 113, 13),
        [25] = (10000, 78, 144, 144, 465, 117, 14),
        [26] = (10000, 81, 150, 150, 498, 121, 15),
        [27] = (10000, 85, 155, 155, 531, 125, 16),
        [28] = (10000, 89, 162, 162, 564, 129, 17),
        [29] = (10000, 92, 168, 168, 597, 133, 18),
        [30] = (10000, 97, 173, 173, 630, 137, 19),
        [31] = (10000, 101, 181, 181, 669, 143, 20),
        [32] = (10000, 106, 188, 188, 708, 148, 22),
        [33] = (10000, 110, 194, 194, 747, 153, 23),
        [34] = (10000, 115, 202, 202, 786, 159, 25),
        [35] = (10000, 119, 209, 209, 825, 165, 27),
        [36] = (10000, 124, 215, 215, 864, 170, 29),
        [37] = (10000, 128, 223, 223, 903, 176, 31),
        [38] = (10000, 134, 229, 229, 942, 181, 33),
        [39] = (10000, 139, 236, 236, 981, 186, 35),
        [40] = (10000, 144, 244, 244, 1020, 192, 38),
        [41] = (10000, 150, 253, 253, 1088, 200, 40),
        [42] = (10000, 155, 263, 263, 1156, 207, 43),
        [43] = (10000, 161, 272, 272, 1224, 215, 46),
        [44] = (10000, 166, 283, 283, 1292, 223, 49),
        [45] = (10000, 171, 292, 292, 1360, 231, 52),
        [46] = (10000, 177, 302, 302, 1428, 238, 55),
        [47] = (10000, 183, 311, 311, 1496, 246, 58),
        [48] = (10000, 189, 322, 322, 1564, 254, 62),
        [49] = (10000, 196, 331, 331, 1632, 261, 66),
        [50] = (10000, 202, 341, 341, 1700, 269, 70),
        [51] = (10000, 204, 342, 366, 0, 0, 0),
        [52] = (10000, 205, 344, 392, 0, 0, 0),
        [53] = (10000, 207, 345, 418, 0, 0, 0),
        [54] = (10000, 209, 346, 444, 0, 0, 0),
        [55] = (10000, 210, 347, 470, 0, 0, 0),
        [56] = (10000, 212, 349, 496, 0, 0, 0),
        [57] = (10000, 214, 350, 522, 0, 0, 0),
        [58] = (10000, 215, 351, 548, 0, 0, 0),
        [59] = (10000, 217, 352, 574, 0, 0, 0),
        [60] = (10000, 218, 354, 600, 0, 0, 0),
        [61] = (10000, 224, 355, 630, 0, 0, 0),
        [62] = (10000, 228, 356, 660, 0, 0, 0),
        [63] = (10000, 236, 357, 690, 1700, 0, 0),
        [64] = (10000, 244, 358, 720, 0, 0, 0),
        [65] = (10000, 252, 359, 750, 0, 0, 0),
        [66] = (10000, 260, 360, 780, 0, 0, 0),
        [67] = (10000, 268, 361, 810, 0, 0, 0),
        [68] = (10000, 276, 362, 840, 0, 0, 0),
        [69] = (10000, 284, 363, 870, 0, 0, 0),
        [70] = (10000, 292, 364, 900, 1700, 0, 0),
        [71] = (10000, 296, 365, 940, 0, 0, 0),
        [72] = (10000, 300, 366, 980, 0, 0, 0),
        [73] = (10000, 305, 367, 1020, 0, 0, 0),
        [74] = (10000, 310, 368, 1060, 0, 0, 0),
        [75] = (10000, 315, 370, 1100, 0, 0, 0),
        [76] = (10000, 320, 372, 1140, 0, 0, 0),
        [77] = (10000, 325, 374, 1180, 0, 0, 0),
        [78] = (10000, 330, 376, 1220, 0, 0, 0),
        [79] = (10000, 335, 378, 1260, 0, 0, 0),
        [80] = (10000, 340, 380, 1300, 2000, 0, 0),
        [81] = (10000, 345, 382, 1360, 2100, 0, 0),
        [82] = (10000, 350, 384, 1420, 2200, 0, 0),
        [83] = (10000, 355, 386, 1480, 2300, 0, 0),
        [84] = (10000, 360, 388, 1540, 2400, 0, 0),
        [85] = (10000, 365, 390, 1600, 2500, 0, 0),
        [86] = (10000, 370, 392, 1660, 2600, 0, 0),
        [87] = (10000, 375, 394, 1720, 2700, 0, 0),
        [88] = (10000, 380, 396, 1780, 2800, 0, 0),
        [89] = (10000, 385, 398, 1840, 2900, 0, 0),
        [90] = (10000, 390, 400, 1900, 3000, 0, 0),
        [91] = (10000, 395, 403, 1988, 3100, 0, 0),
        [92] = (10000, 400, 405, 2076, 3200, 0, 0),
        [93] = (10000, 405, 406, 2164, 3300, 0, 0),
        [94] = (10000, 410, 408, 2252, 3400, 0, 0),
        [95] = (10000, 415, 410, 2340, 3500, 0, 0),
        [96] = (10000, 420, 412, 2428, 3600, 0, 0),
        [97] = (10000, 425, 414, 2516, 3700, 0, 0),
        [98] = (10000, 430, 416, 2604, 3800, 0, 0),
        [99] = (10000, 435, 418, 2692, 3900, 0, 0),
        [100] = (10000, 440, 420, 2780, 4000, 0, 0),
    });
    private static bool LevelIsValid(int level) => level >= 1 && level <= _levelTable.Count;
    /// <summary>
    /// MP
    /// </summary>
    /// <param name="level">Current level</param>
    /// <returns>MP</returns>
    public static int MP(int level) => LevelIsValid(level) ? _levelTable[level].MP : 0;
    /// <inheritdoc cref="IntMAIN"/>
    public static double MAIN(int level) => IntMAIN(level);
    /// <summary>
    /// Main stat base value
    /// </summary>
    /// <param name="level">Current level</param>
    /// <returns>Main</returns>
    public static int IntMAIN(int level) => LevelIsValid(level) ? _levelTable[level].MAIN : 0;
    
    /// <inheritdoc cref="IntSUB"/>
    public static double SUB(int level) => IntSUB(level);
    
    /// <summary>
    /// Sub stat base value
    /// </summary>
    /// <param name="level">Current level</param>
    /// <returns>Sub</returns>
    public static int IntSUB(int level) => LevelIsValid(level) ? _levelTable[level].SUB : 0;
    /// <summary>
    /// The DIV value is returned as double by default to prevent unintentional rounding on division.
    /// See <see cref="IntDIV(int)"/> for integer variant.
    /// </summary>
    /// <param name="level">Level to get Value for.</param>
    /// <returns>DIV value for level</returns>
    public static double DIV(int level) => IntDIV(level);
    /// <summary>
    /// Divider value for this level
    /// </summary>
    /// <param name="level">Current level</param>
    /// <returns>Div</returns>
    public static int IntDIV(int level) => LevelIsValid(level) ? _levelTable[level].DIV : 0;
    
    /// <summary>
    /// HP base value
    /// </summary>
    /// <param name="level">Current level</param>
    /// <returns>HP</returns>
    public static int HP(int level) => LevelIsValid(level) ? _levelTable[level].HP : 0;
    
    /// <summary>
    /// Elemental def base
    /// </summary>
    /// <param name="level">Current level</param>
    /// <returns>Elmt</returns>
    public static int ELMT(int level) => LevelIsValid(level) ? _levelTable[level].ELMT : 0;
    
    /// <summary>
    /// Threat
    /// </summary>
    /// <param name="level">Current level</param>
    /// <returns>Threat</returns>
    public static int THREAT(int level) => LevelIsValid(level) ? _levelTable[level].THREAT : 0;
}
