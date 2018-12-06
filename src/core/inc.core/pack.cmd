dotnet pack ./inc.core/inc.core.csproj --include-symbols -c release --output ../build/inc.core
dotnet pack ./inc.protocols.finsnet/inc.protocols.finsnet.csproj --include-symbols -c release --output ../build/inc.protocols.finsnet
dotnet pack ./inc.protocols.mc/inc.protocols.mc.csproj --include-symbols -c release --output ../build/inc.protocols.mc
dotnet pack ./inc.protocols.modbus/inc.protocols.modbus.csproj --include-symbols -c release --output ../build/inc.protocols.modbus
dotnet pack ./inc.plcs.omron/inc.plcs.omron.csproj --include-symbols -c release --output ../build/inc.plcs.omron