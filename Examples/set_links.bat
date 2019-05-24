echo off

rem MAKE LOOP INSTEAD OF REPEATING FOR EACH DIRECTORY

pushd .

cd StateEngineDemo
cd Assets

IF not exist StateEngine (
    md StateEngine
)
cd StateEngine

mklink /D Prefabs "..\..\..\..\Engine\Prefabs
mklink /D Resources "..\..\..\..\Engine\Resources
mklink /D Scripts "..\..\..\..\Engine\Scripts
mklink /D Tests "..\..\..\..\Engine\Tests

popd


pushd .

cd StateEngineDemoExtended
cd Assets

IF not exist StateEngine (
    md StateEngine
)
cd StateEngine

mklink /D Prefabs "..\..\..\..\Engine\Prefabs
mklink /D Resources "..\..\..\..\Engine\Resources
mklink /D Scripts "..\..\..\..\Engine\Scripts
mklink /D Tests "..\..\..\..\Engine\Tests

cd ..
cd ..
cd ..

popd
