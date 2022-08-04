

function AboutSect(props) {
    return (<div className="row marg">
        <div className="col-xl">
            <h3>{props.text}</h3>
        </div>
        <div className="col-xl">
            <img src={props.src} />
        </div>
    </div>);
}

export default AboutSect;