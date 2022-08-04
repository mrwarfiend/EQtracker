import Card from '../components/Card.jsx';


function Main() {

    var review1 = 'EQTrack uses cutting edge technology to keep us on track for all our equipment needs!';
    var review2 = 'EQTrack team is the most professional tool search and implementation team in the tool business.';
    var review3 = 'EQTrack was well supplied and right on time with all of our tool needs. We completed one of our largest production models thanks to EQtrack!';

    return (<div>
        <div className="container-fluid p0 shadow round autoHeight" style={{ backgroundImage: "url('/images/bpp-client-wall.png')" }}>
            <div className="bgShade shadow round pad text-center">
                <h1 className="txtShadow cWhite topMarg">Some of Our Clients!</h1>
                <h2 className="txtShadow cWhite topMarg"> We cater our services to some of the top producing and innovative companies around today!</h2>
                <div className="row">
                    <div className="col-sm topMarg">
                        <Card title="Cloudera" icon="/images/Cloudera.jpg" comment={review1} />
                    </div>
                    <div className="col-sm topMarg">
                        <Card title="Mitsubishi" icon="/images/mitsubishi.png" comment={review2} />
                    </div>
                    <div className="col-sm topMarg">
                        <Card title="Nuon" icon="/images/nuonlogo.gif" comment={review3} />
                    </div>
                </div>
            </div>
        </div>
    </div>)
}

export default Main;