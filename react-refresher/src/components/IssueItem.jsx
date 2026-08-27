function IssueItem({ issue}) {
    return (
        <li>
            <strong>{issue.title}</strong>
            {' - '}
            {issue.status}
        </li>
    )
}

export default IssueItem