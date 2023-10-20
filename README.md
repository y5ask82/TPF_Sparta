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
<img src="https://img.shields.io/badge/Github-181717?style=flat&logo=github&logoColor=white" />  <img src="https://img.shields.io/badge/Notion-F8F2E9?style=flat&logo=notion&logoColor=white" />

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

#  구성원 소개
<img src="https://img.shields.io/badge/김경원-000000?style=for-the-badge&logo=googlebard&logoColor=darkpink" /><br/>


<img src="https://img.shields.io/badge/박종수-58A616?style=for-the-badge&logo=googlebard&logoColor=white" /><br/>


<img src="https://img.shields.io/badge/김강현-FFB71B?style=for-the-badge&logo=googlebard&logoColor=purple" /><br/>


<img src="https://img.shields.io/badge/이경현-5056E5?style=for-the-badge&logo=googlebard&logoColor=yellow" /><br/>


<img src="https://img.shields.io/badge/조병우-FF7F7F?style=for-the-badge&logo=googlebard&logoColor=darkgray" /><br/>
</div>

# 완성 소감

김경원: 처음에는 진짜 아무것도 못할줄 알았다. 팀원들이랑 튜터님께 귀찮을 정도(?)로 많이 질문도 하고 깃 활용도 엄청 해메고 잘 몰랐는데 점차 조금씩 터득하게 되었고 요번 팀프로젝트를 통해서 실력이 조금이라도 늘었다는 느낌이 든다. 다들 너무 감사하고 최종 프로젝트때 꼭 도 조금씩 도움이 됬으면 좋겠다. 

박종수: 많은 회의를 통해 원활한 소통이 되어서 팀 프로젝트에 막힘이 없었다. 회의 중요성을 깨달았다. 맡은 기능이 잘 구현되어서 좋다.

김강현: 맡은 기능을 생각한 대로 기능이 구현되서 만족스럽다.

이경현: 3D는 처음이라 많이 헤맸지만 맡은 기능은 잘 구현한 것 같아서 다행이다.

조병우: 처음엔 3D에 네비게이션 기능 등 처음해보는 것들이라 걱정했지만 결과적으로 모두 완성할 수 있었고, 그만큼 제 실력도 늘어난 것 같아 자신 또한 생겼습니다. 이대로 최종 프로젝트에서도 많은 것을 만들고 싶습니다.

