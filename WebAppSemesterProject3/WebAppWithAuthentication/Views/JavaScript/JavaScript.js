// styles.js

// Add styling to the table headers
document.querySelectorAll('.table th').forEach(header => {
    header.style.backgroundColor = '#f2f2f2';
    header.style.padding = '10px';
});

// Add styling to the table rows
document.querySelectorAll('.table tbody tr').forEach(row => {
    row.style.borderBottom = '1px solid #ddd';
});

// Add hover effect to table rows
document.querySelectorAll('.table tbody tr').forEach(row => {
    row.addEventListener('mouseover', () => {
        row.style.backgroundColor = '#f9f9f9';
    });

    row.addEventListener('mouseout', () => {
        row.style.backgroundColor = '';
    });
});

// Style links in the table
document.querySelectorAll('.table a').forEach(link => {
    link.style.color = 'blue';
    link.style.textDecoration = 'none';
    link.style.fontWeight = 'bold';

    // Hover effect for links
    link.addEventListener('mouseover', () => {
        link.style.textDecoration = 'underline';
    });

    link.addEventListener('mouseout', () => {
        link.style.textDecoration = 'none';
    });
});
