echo off

rem MAKE LOOP INSTEAD OF REPEATING FOR EACH DIRECTORY

cd StateEngineDemo
cd Assets

md StateEngine
cd StateEngine

mklink /D Prefabs "..\..\..\..\Engine\Prefabs
mklink /D Resources "..\..\..\..\Engine\Resources
mklink /D Scripts "..\..\..\..\Engine\Scripts
mklink /D Tests "..\..\..\..\Engine\Tests

cd ..
cd ..
cd ..


cd StateEngineDemoExtended
cd Assets

md StateEngine
cd StateEngine

mklink /D Prefabs "..\..\..\..\Engine\Prefabs
mklink /D Resources "..\..\..\..\Engine\Resources
mklink /D Scripts "..\..\..\..\Engine\Scripts
mklink /D Tests "..\..\..\..\Engine\Tests

cd ..
cd ..
cd ..
