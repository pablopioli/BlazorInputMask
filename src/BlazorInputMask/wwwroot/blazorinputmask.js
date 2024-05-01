export async function ApplyMaskToElement(id, mask, dotnetHelper) {
    if (globalThis.IMaskPromise == undefined) {
        globalThis.IMaskPromise = new Promise(function (resolve, reject) {
            var script = document.createElement("script");
            script.src = "./_content/PPioli.BlazorInputMask/imask.7.6.0.min.js";
            script.type = "text/javascript";
            script.onload = function () {
                globalThis.IMaskLoaded = true;
                resolve();
            };

            document["body"].appendChild(script);
        });
    }

    await globalThis.IMaskPromise;

    IMask(
        document.getElementById(id), {
        mask: mask,
        commit: function (_value, masked) {
            dotnetHelper.invokeMethodAsync('ReturnUnmaskedValue', masked.unmaskedValue);
        }
    });
}
