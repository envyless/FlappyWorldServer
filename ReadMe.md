
Readme Link  
https://www.notion.so/FlappyWorld-8b14ae66b994407ab428819163e27f47  


#proto3 사용하기 앞서

dotnet add package Google.ProtocolBuffers
dotnet add package Google.ProtocolBuf 

#cli로 위 명령을 통해서 페키지 넣음.

#메세지 팩등을 고려해보자, 클라서버 둘다어짜피 c#인데 굳이 프로토를쓴건 걍써보고싶어서이다.

#프로토 빌드 && Cp To Assets In Client
protoc *.proto --csharp_out=./ServerClientClass
cp -r ./ServerClientClass /mnt/f/git_repo/FlappyWorld/Assets/



