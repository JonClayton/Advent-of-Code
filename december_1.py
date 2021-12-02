def first(input_array):
    answer = 0
    previous = input_array[0]
    for current in input_array:
        if current > previous:
            answer += 1
        previous = current
    return answer


def second(input_array):
    answer = 0
    for current in range(3, len(input_array)):
        if input_array[current] > input_array[current - 3]:
            answer += 1
    return answer


the_big_array = [109, 117, 118, 98, 102, 94, 101, 109, 121, 126, 117, 116, 134, 119, 98, 97, 96, 98, 101, 107, 123, 134,
                 137, 160, 171, 188, 189, 188, 169, 163, 182, 184, 187, 184, 196, 199, 218, 222, 223, 218, 214, 220,
                 215, 223, 226, 228, 236, 237, 268, 277, 282, 284, 285, 288, 289, 313, 322, 331, 335, 331, 316, 323,
                 314, 313, 314, 301, 302, 320, 315, 323, 324, 346, 347, 362, 376, 377, 378, 379, 381, 391, 392, 395,
                 391, 393, 399, 401, 403, 428, 423, 422, 427, 430, 432, 431, 433, 449, 456, 455, 456, 457, 456, 460,
                 461, 460, 459, 460, 467, 468, 505, 506, 513, 505, 509, 510, 511, 521, 522, 523, 525, 521, 514, 517,
                 510, 507, 534, 543, 554, 552, 578, 587, 593, 598, 591, 592, 579, 580, 576, 594, 593, 594, 598, 610,
                 611, 609, 607, 606, 609, 616, 617, 605, 604, 616, 628, 641, 642, 643, 644, 641, 652, 645, 637, 652,
                 651, 648, 649, 647, 650, 651, 652, 660, 675, 672, 643, 624, 633, 635, 626, 642, 645, 651, 658, 671,
                 674, 684, 687, 668, 675, 650, 662, 680, 679, 701, 704, 716, 720, 727, 736, 734, 738, 735, 742, 754,
                 781, 777, 778, 792, 795, 797, 796, 816, 819, 829, 839, 846, 839, 864, 865, 868, 869, 873, 878, 876,
                 878, 881, 882, 909, 922, 929, 944, 945, 944, 945, 954, 958, 959, 963, 967, 960, 957, 965, 986, 1006,
                 986, 1007, 1010, 1011, 1000, 1001, 970, 974, 972, 974, 982, 990, 988, 1002, 1003, 1027, 1037, 1034,
                 1045, 1036, 1038, 1036, 1016, 1015, 1032, 1038, 1040, 1041, 1043, 1046, 1088, 1105, 1112, 1113, 1114,
                 1117, 1132, 1130, 1132, 1121, 1124, 1112, 1117, 1118, 1122, 1123, 1154, 1155, 1153, 1150, 1151, 1167,
                 1171, 1172, 1173, 1174, 1181, 1183, 1193, 1194, 1227, 1231, 1234, 1253, 1256, 1255, 1242, 1255, 1276,
                 1280, 1297, 1298, 1308, 1309, 1307, 1325, 1327, 1329, 1325, 1319, 1323, 1324, 1327, 1361, 1364, 1368,
                 1360, 1365, 1348, 1357, 1358, 1359, 1372, 1373, 1377, 1381, 1383, 1385, 1384, 1387, 1382, 1381, 1410,
                 1405, 1412, 1413, 1416, 1417, 1423, 1434, 1437, 1442, 1443, 1445, 1460, 1461, 1459, 1479, 1452, 1475,
                 1492, 1503, 1508, 1507, 1479, 1480, 1472, 1471, 1478, 1479, 1456, 1459, 1469, 1457, 1460, 1465, 1469,
                 1477, 1478, 1482, 1485, 1486, 1485, 1463, 1473, 1477, 1480, 1482, 1493, 1494, 1495, 1507, 1502, 1501,
                 1496, 1494, 1495, 1493, 1490, 1497, 1521, 1530, 1524, 1525, 1521, 1529, 1545, 1529, 1526, 1538, 1525,
                 1505, 1506, 1511, 1512, 1513, 1512, 1515, 1548, 1555, 1557, 1575, 1576, 1598, 1596, 1609, 1605, 1608,
                 1621, 1631, 1632, 1645, 1647, 1660, 1665, 1693, 1703, 1697, 1699, 1711, 1701, 1702, 1703, 1715, 1716,
                 1717, 1693, 1692, 1697, 1669, 1670, 1673, 1690, 1697, 1705, 1707, 1726, 1720, 1721, 1726, 1732, 1731,
                 1739, 1749, 1737, 1713, 1718, 1734, 1735, 1742, 1745, 1742, 1741, 1742, 1743, 1776, 1779, 1797, 1801,
                 1802, 1804, 1806, 1807, 1803, 1804, 1806, 1817, 1815, 1802, 1803, 1809, 1811, 1814, 1811, 1815, 1817,
                 1818, 1817, 1808, 1819, 1824, 1829, 1830, 1817, 1819, 1820, 1821, 1822, 1823, 1827, 1828, 1829, 1832,
                 1834, 1839, 1845, 1848, 1857, 1859, 1879, 1880, 1885, 1888, 1887, 1912, 1910, 1917, 1929, 1930, 1931,
                 1932, 1930, 1914, 1917, 1904, 1910, 1913, 1901, 1903, 1904, 1900, 1896, 1917, 1921, 1910, 1914, 1915,
                 1917, 1915, 1916, 1939, 1940, 1941, 1942, 1943, 1946, 1947, 1962, 1946, 1949, 1938, 1935, 1927, 1928,
                 1931, 1932, 1933, 1931, 1957, 1951, 1956, 1957, 1958, 1969, 1967, 1966, 1992, 1991, 1997, 1999, 2010,
                 2016, 2039, 2043, 2047, 2048, 2046, 2064, 2069, 2072, 2084, 2094, 2103, 2104, 2105, 2080, 2090, 2092,
                 2079, 2094, 2100, 2097, 2105, 2104, 2092, 2093, 2094, 2100, 2105, 2106, 2122, 2126, 2128, 2134, 2141,
                 2153, 2154, 2157, 2170, 2171, 2158, 2156, 2180, 2181, 2167, 2182, 2188, 2166, 2162, 2167, 2163, 2181,
                 2191, 2192, 2195, 2237, 2236, 2249, 2255, 2258, 2271, 2272, 2257, 2260, 2263, 2264, 2281, 2282, 2285,
                 2289, 2293, 2279, 2284, 2287, 2292, 2291, 2303, 2304, 2302, 2306, 2324, 2323, 2327, 2330, 2321, 2332,
                 2316, 2320, 2323, 2336, 2332, 2303, 2304, 2303, 2304, 2308, 2311, 2324, 2325, 2347, 2348, 2345, 2340,
                 2348, 2347, 2348, 2346, 2352, 2345, 2346, 2347, 2350, 2354, 2359, 2363, 2369, 2374, 2378, 2377, 2393,
                 2400, 2403, 2405, 2404, 2411, 2417, 2415, 2408, 2407, 2409, 2419, 2420, 2446, 2448, 2450, 2451, 2446,
                 2447, 2448, 2450, 2451, 2449, 2448, 2450, 2453, 2454, 2460, 2463, 2465, 2474, 2471, 2473, 2471, 2485,
                 2487, 2483, 2501, 2494, 2519, 2517, 2516, 2517, 2520, 2523, 2524, 2489, 2491, 2494, 2493, 2504, 2507,
                 2512, 2513, 2511, 2512, 2488, 2495, 2484, 2476, 2477, 2480, 2479, 2487, 2495, 2492, 2493, 2513, 2512,
                 2506, 2513, 2514, 2515, 2499, 2506, 2510, 2505, 2506, 2512, 2513, 2515, 2527, 2515, 2516, 2533, 2534,
                 2535, 2536, 2541, 2548, 2586, 2588, 2601, 2604, 2614, 2616, 2615, 2622, 2624, 2639, 2638, 2642, 2647,
                 2637, 2654, 2613, 2614, 2618, 2609, 2608, 2609, 2613, 2634, 2631, 2651, 2658, 2660, 2670, 2674, 2675,
                 2678, 2682, 2686, 2680, 2683, 2689, 2691, 2690, 2691, 2697, 2700, 2704, 2716, 2720, 2723, 2737, 2734,
                 2737, 2736, 2737, 2740, 2741, 2743, 2737, 2738, 2737, 2746, 2752, 2748, 2749, 2755, 2756, 2758, 2774,
                 2784, 2785, 2800, 2802, 2811, 2813, 2825, 2819, 2822, 2823, 2829, 2824, 2823, 2824, 2805, 2808, 2806,
                 2821, 2820, 2826, 2834, 2833, 2837, 2850, 2853, 2857, 2869, 2868, 2870, 2891, 2890, 2895, 2896, 2905,
                 2926, 2928, 2926, 2930, 2929, 2938, 2970, 2976, 2968, 2967, 2988, 3000, 3001, 3002, 3003, 3020, 3021,
                 3025, 3035, 3041, 3040, 3014, 3013, 3019, 3024, 3025, 3035, 3036, 3032, 3054, 3065, 3064, 3053, 3054,
                 3058, 3052, 3061, 3063, 3064, 3068, 3096, 3095, 3091, 3114, 3124, 3130, 3137, 3146, 3147, 3148, 3142,
                 3144, 3145, 3150, 3151, 3149, 3158, 3146, 3144, 3135, 3153, 3166, 3168, 3167, 3151, 3147, 3155, 3156,
                 3159, 3156, 3159, 3160, 3175, 3174, 3188, 3182, 3175, 3185, 3189, 3194, 3169, 3173, 3176, 3177, 3169,
                 3137, 3138, 3148, 3139, 3141, 3142, 3162, 3163, 3168, 3164, 3168, 3167, 3176, 3173, 3197, 3198, 3199,
                 3216, 3232, 3245, 3272, 3295, 3294, 3291, 3301, 3303, 3304, 3306, 3307, 3306, 3312, 3291, 3292, 3290,
                 3266, 3271, 3261, 3267, 3268, 3288, 3300, 3289, 3297, 3264, 3271, 3255, 3256, 3263, 3266, 3269, 3308,
                 3323, 3320, 3326, 3332, 3334, 3335, 3341, 3369, 3375, 3365, 3359, 3362, 3365, 3370, 3371, 3364, 3365,
                 3366, 3375, 3376, 3392, 3393, 3428, 3458, 3474, 3475, 3484, 3490, 3496, 3512, 3510, 3512, 3513, 3516,
                 3517, 3540, 3550, 3549, 3540, 3552, 3559, 3549, 3550, 3568, 3569, 3568, 3572, 3576, 3578, 3580, 3581,
                 3583, 3586, 3590, 3574, 3576, 3579, 3563, 3558, 3557, 3554, 3556, 3564, 3561, 3563, 3583, 3582, 3590,
                 3594, 3583, 3578, 3596, 3602, 3615, 3621, 3625, 3631, 3633, 3655, 3636, 3639, 3640, 3641, 3644, 3646,
                 3652, 3655, 3671, 3681, 3685, 3692, 3673, 3691, 3696, 3692, 3695, 3704, 3719, 3721, 3728, 3737, 3738,
                 3754, 3757, 3754, 3759, 3758, 3765, 3767, 3770, 3774, 3769, 3756, 3755, 3750, 3780, 3782, 3781, 3785,
                 3788, 3771, 3774, 3775, 3764, 3742, 3743, 3755, 3759, 3757, 3767, 3771, 3770, 3776, 3778, 3773, 3774,
                 3775, 3778, 3777, 3764, 3773, 3767, 3768, 3769, 3792, 3810, 3808, 3791, 3789, 3790, 3816, 3838, 3841,
                 3849, 3850, 3849, 3853, 3854, 3878, 3880, 3892, 3911, 3886, 3885, 3889, 3892, 3893, 3918, 3919, 3915,
                 3916, 3913, 3928, 3955, 3956, 3961, 3959, 3963, 3962, 3964, 3973, 3969, 3972, 3973, 3974, 3993, 3995,
                 3998, 4018, 4026, 4027, 4026, 4027, 4024, 4039, 4052, 4055, 4056, 4060, 4077, 4069, 4066, 4067, 4071,
                 4102, 4099, 4106, 4112, 4114, 4125, 4130, 4127, 4157, 4147, 4148, 4127, 4130, 4131, 4130, 4129, 4134,
                 4124, 4132, 4133, 4134, 4153, 4154, 4157, 4154, 4187, 4168, 4165, 4170, 4172, 4205, 4203, 4204, 4174,
                 4175, 4172, 4169, 4171, 4198, 4203, 4215, 4209, 4212, 4213, 4229, 4230, 4232, 4233, 4237, 4242, 4243,
                 4268, 4277, 4278, 4284, 4293, 4298, 4302, 4281, 4282, 4280, 4271, 4264, 4265, 4258, 4269, 4268, 4273,
                 4276, 4284, 4270, 4271, 4266, 4275, 4305, 4309, 4314, 4316, 4324, 4323, 4335, 4338, 4339, 4368, 4369,
                 4368, 4373, 4375, 4366, 4370, 4376, 4375, 4374, 4377, 4378, 4379, 4380, 4392, 4395, 4391, 4389, 4390,
                 4391, 4400, 4393, 4391, 4396, 4364, 4351, 4385, 4387, 4391, 4395, 4387, 4407, 4408, 4410, 4426, 4449,
                 4450, 4460, 4461, 4460, 4465, 4469, 4472, 4479, 4489, 4492, 4508, 4512, 4527, 4537, 4544, 4552, 4553,
                 4561, 4566, 4567, 4561, 4559, 4550, 4543, 4549, 4554, 4557, 4558, 4570, 4585, 4595, 4597, 4584, 4583,
                 4605, 4603, 4606, 4607, 4608, 4612, 4611, 4610, 4620, 4621, 4613, 4615, 4605, 4617, 4626, 4650, 4655,
                 4665, 4664, 4663, 4665, 4669, 4668, 4670, 4662, 4666, 4665, 4660, 4661, 4673, 4676, 4679, 4685, 4687,
                 4708, 4704, 4707, 4717, 4718, 4716, 4724, 4715, 4718, 4721, 4714, 4738, 4739, 4740, 4753, 4756, 4757,
                 4763, 4754, 4755, 4747, 4760, 4761, 4760, 4781, 4774, 4776, 4784, 4786, 4785, 4783, 4798, 4800, 4796,
                 4795, 4798, 4800, 4801, 4805, 4794, 4800, 4819, 4823, 4824, 4822, 4816, 4820, 4826, 4831, 4830, 4832,
                 4833, 4827, 4822, 4826, 4825, 4824, 4823, 4824, 4844, 4849, 4853, 4858, 4861, 4859, 4856, 4841, 4843,
                 4844, 4843, 4858, 4859, 4864, 4860, 4857, 4869, 4901, 4902, 4907, 4908, 4909, 4911, 4914, 4918, 4906,
                 4907, 4912, 4902, 4911, 4888, 4890, 4888, 4885, 4887, 4873, 4874, 4880, 4891, 4890, 4891, 4892, 4909,
                 4910, 4930, 4932, 4937, 4940, 4948, 4950, 4945, 4948, 4949, 4950, 4946, 4945, 4936, 4954, 4955, 4942,
                 4952, 4956, 4959, 4958, 4960, 4958, 4964, 4972, 4974, 4973, 4974, 4988, 4989, 4956, 4960, 4959, 4966,
                 4968, 4961, 4965, 4971, 4979, 4983, 4990, 4991, 4990, 4993, 4997, 5016, 5010, 5008, 5009, 4997, 4998,
                 4999, 5008, 5021, 5022, 5023, 5024, 5030, 5014, 5017, 5022, 5018, 5017, 5021, 5023, 5002, 5003, 5004,
                 5010, 4998, 4999, 5008, 5007, 5011, 5013, 5039, 5041, 5043, 5046, 5057, 5055, 5017, 5015, 5010, 5011,
                 5017, 5011, 5019, 5020, 5022, 5023, 5032, 5033, 5042, 5062, 5083, 5092, 5093, 5081, 5082, 5085, 5086,
                 5085, 5087, 5099, 5096, 5085, 5088, 5087, 5090, 5091, 5104, 5092, 5093, 5081, 5080, 5082, 5086, 5087,
                 5088, 5098, 5097, 5101, 5114, 5117, 5120, 5128, 5129, 5126, 5127, 5128, 5129, 5135, 5138, 5142, 5145,
                 5146, 5169, 5170, 5172, 5168, 5172, 5189, 5190, 5201, 5222, 5219, 5220, 5237, 5238, 5245, 5286, 5280,
                 5293, 5294, 5306, 5305, 5308, 5317, 5320, 5322, 5347, 5363, 5361, 5359, 5365, 5373, 5375, 5379, 5378,
                 5379, 5381, 5388, 5386, 5389, 5401, 5402, 5401, 5403, 5409, 5404, 5409, 5414, 5415, 5434, 5436, 5444,
                 5448, 5465, 5464, 5465, 5466, 5468, 5469, 5456, 5460, 5443, 5445, 5443, 5439, 5438, 5445, 5452, 5484,
                 5487, 5481, 5488, 5489, 5504, 5513, 5536, 5539, 5547, 5571, 5584, 5597, 5601, 5572, 5575, 5582, 5583,
                 5586, 5591, 5590, 5603, 5598, 5603, 5588, 5570, 5571, 5573, 5575, 5594, 5595, 5599, 5602, 5610, 5641,
                 5639, 5647, 5656, 5663, 5661, 5664, 5651, 5645, 5650, 5658, 5659, 5660, 5655, 5654, 5655, 5673, 5672,
                 5655, 5653, 5661, 5666, 5669, 5671, 5675, 5681, 5692, 5685, 5684, 5685, 5684, 5724, 5725, 5732, 5731,
                 5732, 5733, 5728, 5743, 5742, 5767, 5775, 5779, 5783, 5774, 5778, 5783, 5795, 5797, 5801, 5795, 5794,
                 5795, 5790, 5789, 5791, 5789, 5791, 5792, 5788, 5789, 5798, 5799, 5792, 5802, 5801, 5808, 5805, 5806,
                 5799, 5795, 5812, 5816, 5818, 5794, 5779, 5781, 5792, 5799, 5814, 5805, 5809, 5811, 5813, 5816, 5820,
                 5835, 5830, 5829, 5857, 5862, 5863, 5854, 5849, 5852, 5855, 5853, 5852, 5853, 5857, 5858, 5867, 5863,
                 5865, 5864, 5875, 5900, 5874, 5883, 5886, 5915, 5913, 5914, 5882, 5888, 5889, 5890, 5891, 5880, 5883,
                 5901, 5903, 5904, 5907, 5910, 5913, 5928, 5954, 5963, 5964, 5967, 5975, 5976, 5973, 5979, 5980, 5979,
                 5990, 5989, 6002, 5998, 6006, 6012, 6007, 6014, 6019, 6020, 6019, 6008, 6022, 6032, 6028, 6010, 6009,
                 6022, 6023, 6025, 6030, 6037, 6038, 6041, 6035, 6025, 6020, 6036, 6026, 6027, 6032, 6041, 6034, 6036,
                 6044, 6055, 6061, 6101, 6102, 6123, 6128, 6129, 6131, 6141, 6142, 6143, 6144, 6143, 6140, 6143, 6153,
                 6154, 6168, 6170, 6167, 6174, 6180, 6160, 6162, 6167, 6163, 6160, 6167, 6166, 6167, 6164, 6153, 6155,
                 6185, 6193, 6170, 6190, 6201, 6238, 6225, 6223, 6222, 6225, 6227, 6232, 6229, 6230, 6234, 6239, 6240,
                 6254, 6253, 6240, 6252, 6273, 6266, 6276, 6277, 6276, 6299, 6300, 6299, 6289, 6291, 6289, 6287, 6288,
                 6287, 6289, 6292, 6293, 6297, 6308]

