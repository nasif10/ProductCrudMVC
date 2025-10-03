// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Toast(heading, text, icon) {
	$.toast({
		heading: heading,
		text: text,
		showHideTransition: 'slide',
		icon: icon,
		position: 'top-right',
		stack: false
	});
}