

function Card(props) {
    return (<div className="card shadow">
        <div className="row cardHeader">
            <div className="col-8 cardTitle">
                <h5 className="cardTitleText">{props.title}</h5>
            </div>
            <div className="col-4 cardLogo">
                <img className="cardLogoImg" src={props.icon} alt="logo" />
            </div>
        </div>
        <div className="row cardComment">
            <h5 className="cardCommentText">{props.comment}</h5>
        </div>
    </div>)
}

export default Card;