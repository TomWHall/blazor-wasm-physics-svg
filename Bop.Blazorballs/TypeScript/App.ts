import {DotNet} from "@microsoft/dotnet-js-interop";
import DotNetObject = DotNet.DotNetObject;

let gameHost: DotNetObject;

function animate(time: number): void {
    requestAnimationFrame(animate);

    gameHost.invokeMethodAsync('TickGame').then(() => {});
}

(window as any).Blazorballs = {
    startGameLoop: function(gameHostObj: DotNetObject) {
        console.log('startGameLoop enter');
        gameHost = gameHostObj;

        animate(performance.now());
    }
};
