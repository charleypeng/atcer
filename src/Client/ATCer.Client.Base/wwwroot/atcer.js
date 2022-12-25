//To set the ex
export function setTitle(title)
{
    console.log("真的烦");
    window.alert(title);
    return title;
    //document.getElementById('atcer_rank_list').style.animation = 'fading 2s infinite';
}

export function moveButton(elementId) {
    var el = document.getElementById(elementId);

    // Start the animation
    moveEl(el);
}

function moveEl(el) {
    var left = parseInt(el.style.left) || 0;

    el.style.position = 'relative';
    el.style.left = (left + 1) + 'px';

    // Recursive timeout to move the element left 1px every 20ms
    setTimeout(function () {
        moveEl(el);
    }, 20);
}

export function loveUs(msg) {
    var waka = msg + "{热望热望}";
    var oldTitle = document.title;
    document.title = oldTitle + '-' + waka;
    console.log(waka);
    return waka;
}

let timeout
let echarts_instance_cache = {}

export function init(selector, theme, initOptions, option) {
    let instance = echarts_instance_cache[selector]

    if (instance && instance.isDisposed() === false) return;

    const container = document.querySelector(selector)

    if (echarts && container) {
        if (initOptions && initOptions.renderer) {
            initOptions.renderer = initOptions.renderer.toLowerCase()
        }

        instance = echarts.init(container, theme, initOptions);

        echarts_instance_cache[selector] = instance

        instance.setOption(option, true);

        window.addEventListener('resize', () => onResize(instance));
    }
}

export function dispose(selector) {
    const instance = echarts_instance_cache[selector]

    if (instance && instance.dispose) {
        instance.dispose();
        window.removeEventListener('resize', () => onResize(instance))
    }
}

export function setOption(selector, option) {
    const instance = echarts_instance_cache[selector]
    if (instance) {
        instance.setOption(option, true);
    }
}

function onResize(instance) {
    window.clearTimeout(timeout);
    timeout = window.setTimeout(function () {
        if (instance && instance.resize) {
            instance.resize();
        }
    }, 300);
}