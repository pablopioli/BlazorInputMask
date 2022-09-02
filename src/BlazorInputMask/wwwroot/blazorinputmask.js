export function ApplyMaskToElement(id, mask, dotnetHelper) {
    IMask(
        document.getElementById(id), {
        mask: mask,
        commit: function (_value, masked) {
            dotnetHelper.invokeMethodAsync('ReturnUnmaskedValue', masked.unmaskedValue);
        }
    });
}
