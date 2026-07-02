# 階段 1：使用微軟官方的 .NET SDK 進行程式碼編譯
FROM ://microsoft.com AS build-env
WORKDIR /app

# 複製專案檔並進行套件還原
COPY *.csproj ./
RUN dotnet restore

# 複製所有程式碼並發行 Release 版本
COPY . ./
RUN dotnet publish -c Release -o out

# 階段 2：使用輕量級的 .NET 執行環境來跑你的網站
FROM ://microsoft.com
WORKDIR /app
COPY --from=build-env /app/out .

# 設定雲端主機固定對外開啟 8080 連接埠
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# 啟動 C# 程式碼（注意：下面的專案名稱要換成你真實的 .dll 檔名）
ENTRYPOINT ["dotnet", "render-ci.dll"]
