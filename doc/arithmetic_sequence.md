# Arithmetic sequence

https://en.wikipedia.org/wiki/Arithmetic_progression

$$ 
a_n = a_0 + n⋅d
$$

$$
n = \frac{a_n - a_0}{d}
$$

## 주어진 값으로부터 근사 인덱스 구하기

### 주어진 값
- 주어진 상수
- value
- $v$

### 근사 인덱스
- 미지수
- approximate index
- $n_{appr}$

### 근사 수열값
- approximate sequence index
- $a_{n_{appr}}$

### 1. 관계식
$$
-\frac{1}{2} <_1 \frac{v - a_{n_{appr}}}{d} <_2 \frac{1}{2}
$$

$0 < d$ 일 때,
$$
-\frac{d}{2} <_1 v - a_{n_{appr}} <_2 \frac{d}{2}
$$
$$
-\frac{d}{2} - v <_1 - a_{n_{appr}} <_2 \frac{d}{2} - v
$$
$$
-\frac{d}{2} + v <_2 a_{n_{appr}} <_1 \frac{d}{2} + v
$$
$$
-\frac{d}{2} + v <_2 a_0 + n_{appr}⋅d <_1 \frac{d}{2} + v
$$
$$
-\frac{d}{2} + v - a_0 <_2 n_{appr}⋅d <_1 \frac{d}{2} + v - a_0
$$
$$
-\frac{d}{2} + v - a_0 <_2 n_{appr}⋅d <_1 \frac{d}{2} + v - a_0
$$
$$
-\frac{1}{2} + \frac{v - a_0}{d} <_2 n_{appr} <_1 \frac{1}{2} + \frac{v - a_0}{d}
$$
$$
-1 + n_{appr} <_1 -\frac{1}{2} + \frac{v - a_0}{d} <_2 n_{appr} <_1 \frac{1}{2} + \frac{v - a_0}{d}
$$
$$
n_{appr} - \frac{1}{2} <_1 \frac{v - a_0}{d} <_2 n_{appr} + \frac{1}{2}
$$
$$
n_{appr} <_1 \frac{v - a_0}{d} - \frac{1}{2} <_2 n_{appr} + 1
$$
$$
n_{appr}⋅d <_1 v - a_0 - \frac{d}{2} <_2 n_{appr}⋅d + d
$$
$$
2⋅n_{appr}⋅d <_1 2⋅v - 2⋅a_0 - d <_2 2⋅n_{appr}⋅d + 2⋅d
$$
$$
n_{appr} <_1 \frac{2⋅v - 2⋅a_0 - d}{2⋅d} <_2 n_{appr} + 1
$$

### 2. 일반화 관계식

$0 ≤ r < 1$ 일 때,

$$
-r <_1 \frac{v - a_{n_{appr}}}{d} <_2 (1 - r)
$$

$0 < d$ 일 때,

$$
-r⋅d <_1 v - a_{n_{appr}} <_2 (1 - r)⋅d = d - r⋅d
$$
$$
0 <_1 v - a_{n_{appr}} + r⋅d <_2 d
$$
$$
0 <_1 v - a_0 - n_{appr}⋅d + r⋅d <_2 d
$$
$$
n_{appr}⋅d <_1 v - a_0 + r⋅d <_2 n_{appr}⋅d + d = (n_{appr} + 1)⋅d
$$
$$
n_{appr} <_1 \frac{v - a_0}{d} + r <_2 n_{appr} + 1
$$