function IssueItem({ issue, onEdit, onDelete}) {
    return (
        <li>
            <strong>{issue.title}</strong>
            {' - '}
            {issue.status}
            {' - '}
            {issue.categoryName ?? 'Ingen kategori'}

        <button
            type="button"
            onClick={() => onEdit(issue)}
            >
            Redigera
        </button>
        <button
            type="button"
            onClick={() => onDelete(issue.id)}
        >
            Ta bort
        </button>
        </li>
    )
}

export default IssueItem
