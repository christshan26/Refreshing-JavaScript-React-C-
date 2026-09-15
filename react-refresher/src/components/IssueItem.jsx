function IssueItem({ issue, onEdit}) {
    return (
        <li>
            <strong>{issue.title}</strong>
            {' - '}
            {issue.status}

        <button
            type="button"
            onClick={() => onEdit(issue)}
            >
            Redigera
        </button>
        </li>
    )
}

export default IssueItem