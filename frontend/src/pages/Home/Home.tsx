export default function Home() {
    return(
        <div className="flex flex-col items-center bg-gray-100 p-4 min-h-screen fade-in">
            <h1 className="text-3xl font-bold mb-6">Welcome to Project B</h1>
            
            <div className="w-4/5 bg-white p-6 shadow-lg rounded-lg mt-6">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6 items-center mb-6">
                    <img src="/assets/images/homepage1_vehicle.jpg" alt="Vehicle" className="w-full rounded-lg fade-in" />
                    <p className="text-gray-700">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae ipsum viverra, scelerisque magna in, fringilla nulla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Phasellus gravida, orci in elementum ornare, lectus arcu aliquam massa, quis commodo metus diam at arcu. Aliquam dolor arcu, fermentum vel viverra sit amet, blandit pharetra mi. Mauris eget condimentum lacus. Etiam vitae lacinia urna. Donec lacinia nibh quis nulla blandit, eu pretium leo lacinia. Sed id magna sem. Suspendisse pellentesque tristique suscipit. Nunc volutpat tortor et justo vehicula ultricies. Fusce id tortor mattis lorem pulvinar convallis sed et leo.</p>
                </div>
                
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6 items-center mb-6">
                    <p className="text-gray-700">Aenean tincidunt mauris in neque pharetra, sed ullamcorper lorem eleifend. Fusce mattis id lectus id sollicitudin. Aliquam erat volutpat. Donec ac tincidunt sapien, vitae consectetur diam. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed aliquam tristique sapien ac placerat. Duis hendrerit lectus nulla, in mollis quam venenatis nec. Sed eros mi, semper non pharetra at, pulvinar ac lacus. Cras vitae porttitor augue.</p>
                    <img src="/assets/images/homepage2_manycars.jpg" alt="ManyCars" className="w-full rounded-lg fade-in" />
                </div>
                
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6 items-center">
                    <img src="/assets/images/homepage3_japanesecars.jpg" alt="JapaneseCars" className="w-full rounded-lg fade-in" />
                    <p className="text-gray-700">Morbi faucibus urna ut tempor hendrerit. Duis pellentesque quis libero sed convallis. Suspendisse pretium luctus risus, ac suscipit metus tempor sit amet. Suspendisse potenti. Vestibulum pulvinar pulvinar ligula vitae porttitor. Mauris finibus mi faucibus nisl maximus maximus. Aenean eget metus velit. Nullam sit amet hendrerit erat. Duis malesuada pellentesque nisl sed rhoncus. Nunc sed massa eget massa ornare fermentum id sit amet velit. Cras pulvinar eleifend purus. Curabitur sagittis fermentum neque, ut molestie risus vehicula sit amet. Curabitur pulvinar consectetur urna a varius. In hac.</p>
                </div>
            </div>
        </div>
    )
}