<div align="center">
# TPF_Sparta

  #  프로젝트 소개
### 프로젝트 명 
**드림 메이즈 러너**
### 프로젝트 소개  
어느날 꿈속에 갇힌 당신. 주위엔 처음보는 낯선 풍경이다.<br/>
걷다보니 열쇠가 보인다. 다 모으면 꿈속에서 깰 수 있을까?<br/>
쫓아오는 귀신들을 피해 열쇠를 모으고 꿈속에서 탈출하자!

### 게임유형 
미로 탈출형 공포 게임

### 개발기간 
2023.10.13 ~ 2023.10.20

### 기술스택
<img src="https://img.shields.io/badge/CSharp-512BD4?style=flat&logo=csharp&logoColor=white" /> <img src="https://img.shields.io/badge/Unity-000000?style=flat&logo=unity&logoColor=white" /> <img src="https://img.shields.io/badge/VisualStudio-5C2D91?style=flat&logo=visualstudio&logoColor=white" /><br/>
<img src="https://img.shields.io/badge/Github-181717?style=flat&logo=github&logoColor=white" /> <img src="https://img.shields.io/badge/FIGMA-F24E1E?style=flat&logo=figma&logoColor=white" /> <img src="https://img.shields.io/badge/Notion-F8F2E9?style=flat&logo=notion&logoColor=white" />

##  주요 기능 구현
![image](https://github.com/y5ask82/TPF_Sparta/assets/99133865/88c5b681-ea67-4307-aabf-5896af850217)
###  스타트 씬, 게임 시작과 설명 선택 가능.

![image](https://github.com/y5ask82/TPF_Sparta/assets/99133865/80995cf0-23eb-4a50-b103-57346e4ec5c5)
###  설명 클릭시 조작키 설명.

![image](https://github.com/y5ask82/TPF_Sparta/assets/99133865/ee07f1b7-777c-4148-81c6-eec9e61a94be)
###  1인칭 시점. 미로 탐색형

![image](https://github.com/y5ask82/TPF_Sparta/assets/99133865/1f882900-a0b1-4cd4-b325-6d84745c55d5)
###  ESC 클릭 시 일시정지, 설정 변경 가능.

![image](https://github.com/y5ask82/TPF_Sparta/assets/99133865/dd059b4d-c3e3-4f88-870d-ca05a79f547f)
###  밝기와 소리 크기 조절 가능.

![image](https://github.com/y5ask82/TPF_Sparta/assets/99133865/a032c67d-6aeb-4840-b671-259c8e4cf3fa)
### 맵 디자인

#  플레이어 기능
플레이어는 쉬프트 키를 눌러 스태미나가 다 떨어질때까지 질주를 할 수 있습니다.

플레이어가 맵의 중앙에 위치한 안전구역에 진입 시 몬스터들은 비활성화되고 다시 안전지대에서 나올 경우 처음 생성 위치에서 다시 생성됩니다.

플레이어가 키를 획득하면 이번 라운드의 몬스터는 사라지고 다음 라운드의 몬스터가 생성됩니다.

키의 위치는 플레이어의 시야에서 확인할 수 있게 표시되어 플레이어는 그 방향을 목표로 이동할 수 있습니다.

플레이어는 마우스 휠과 Q버튼을 통해 지형에 표식을 남길 수 있고 이는 플레이어가 죽더라도 저장되어 다음 시도에서 활용할 수 있습니다.

# 몬스터 기능
몬스터들은 플레이어 위치를 받아와서 네비게이션 기능을 통해 추적합니다.

몬스터 A의 경우 플레이어의 근처까지는 다가오지만 시야에 보이지 않을 경우 주변을 어슬렁거리게 설정하였고, 시야에 발견 시 플레이어를 다시 쫓아오게 됩니다.

몬스터 B의 경우 플레이어의 위치를 완벽히 알고있으며 벽을 통과할 수 있는 능력이 있지만, 속도가 느립니다.

몬스터 C의 경우 라운드 시작 시 맵 어딘가에 숨어있으며, 플레이어는 이를 찾아서 마지막 열쇠를 받아야 합니다.

모든 열쇠를 얻고나면 타이머가 작동하게 되고 3마리의 몬스터가 모두 쫓아오게 됩니다.
타이머가 0이 되기 전까지 탈출구로 탈출하면 게임을 승리합니다.
이때 몬스터 C의 경우 플레이어를 추적할 때 후레쉬로 몬스터를 조준할 경우 멈추는 기믹을 가지고 있습니다.

몬스터에게는 붉은 빛을 발하는 기능을 추가하여 플레이어가 몬스터의 위치를 찾을 수 있게 합니다.

# 아이템 및 오브젝트 기능

화면 오른쪽 아래에 보이는 섬광탄을 사용하면 플레이어 시야 내의 몬스터가 일정시간 동안 비활성화가 됩니다.

맵에는 붉은색 빛을 발생시키는 함정이 있어 몬스터의 위치를 착각하게 하는 기능이 있습니다.

중앙의 안전지대에 플레이어가 입장 시 초록색 불빛이 켜지며 안전지대라고 인식할 수 있게 설정했습니다.
하지만 마지막 라운드에서 이 시스템은 작동하지 않습니다.

중앙의 안전지대에는 벽이 생성되어 있는데 이는 키를 획득할때마다 하나씩 사라지게 됩니다.

