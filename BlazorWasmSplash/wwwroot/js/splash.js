export function toggleSplashClass() {
    if ( document.documentElement.classList.contains('splash') ) {
        document.documentElement.classList.remove('splash');
    }
    else {
        document.documentElement.classList.add('splash');
    }
}