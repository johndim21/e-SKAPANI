//  SightDetailsViewController.swift
//  Escapani
//
//  Created by Panos Peltekis on 13/4/21.
//

import ImageSlideshow
import MapKit
import UIKit

class SightDetailsViewController: BaseViewController, ViewModelable {
        
    // MARK: - IBOutlets
    
    @IBOutlet weak var sightDetailsTableView: UITableView!
    @IBOutlet weak var tabsCollectionView: UICollectionView!
    
    //MARK: - IBActions

    // MARK: - Properties
    
    var viewModel: SightDetailsViewModel!
    var coordinator: SightDetailsCoordinator?
    var imageSlideShow: ImageSlideshow?
    var selectedSite: DigitalWalkSightType?
    
    // MARK: - Lifecycle
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.navigationController?.navigationBar.backItem?.backButtonTitle = ANLocalizedString("back_button_label", comment: "")
        
        self.view.backgroundColor = .clear
        
        sightDetailsTableView.register(UINib(nibName: "DigitalWalkTableViewCell", bundle: nil), forCellReuseIdentifier: "DigitalWalkTableViewCellIdentifier")
        sightDetailsTableView.register(UINib(nibName: "SightInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "SightInfoTableViewCellIdentifier")
        sightDetailsTableView.register(UINib(nibName: "SightImagesTableViewCell", bundle: nil), forCellReuseIdentifier: "SightImagesTableViewCellIdentifier")
        
        sightDetailsTableView.setContentOffset(CGPoint(x: 0, y: -50), animated: false)
        sightDetailsTableView.backgroundColor = .clear
        sightDetailsTableView.showsVerticalScrollIndicator = false
        sightDetailsTableView.bounces = false
        
        sightDetailsTableView.delegate = self
        sightDetailsTableView.dataSource = self
    
        self.tabsCollectionView.delegate = self
        self.tabsCollectionView.dataSource = self
        self.tabsCollectionView.register(UINib(nibName: "TabsCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "TabsCollectionViewCellIdentifier")
        self.setUpTabsCollectionView()
        
        if let layout = tabsCollectionView.collectionViewLayout as? UICollectionViewFlowLayout {
            layout.scrollDirection = .horizontal
        }
    }
    
    // MARK: - Methods
    
    @objc func didTap() {
        imageSlideShow?.presentFullScreenController(from: self)
    }
    
    @objc func pop() {
        let transition:CATransition = CATransition()
        transition.duration = 0.5
        transition.timingFunction = CAMediaTimingFunction(name:CAMediaTimingFunctionName.easeInEaseOut)
        transition.type = .moveIn
        transition.subtype = .fromBottom
        self.navigationController?.view.layer.add(transition, forKey: kCATransition)
        
        self.navigationController?.popViewController(animated: false)
    }
    
    func openMapForPlace() {
            
        let coordinate = CLLocationCoordinate2DMake(40.632171, 22.951782)
        let mapItem = MKMapItem(placemark: MKPlacemark(coordinate: coordinate, addressDictionary:nil))
        mapItem.name = "Arch of Galerius"
        mapItem.openInMaps(launchOptions: [MKLaunchOptionsDirectionsModeKey : MKLaunchOptionsDirectionsModeDefault])
        }
    
    func setUpTabsCollectionView() {
        self.tabsCollectionView.addShadow(location: .top)
        self.tabsCollectionView.showsHorizontalScrollIndicator = false
        self.tabsCollectionView.showsVerticalScrollIndicator = false
        self.tabsCollectionView.bounces = false
    }
}

extension SightDetailsViewController: Storyboardable {
    static var storyboardName: String = "SightDetails"
    static var storyboardIdentifier: String? = "SightDetailsViewControllerIdentifier"
}

extension SightDetailsViewController: DigitalWalkTableViewCellDelegate {
    func setUpSightDetails(sight: DigitalWalkSightType) {
        self.viewModel.sight = sight
        self.sightDetailsTableView.reloadData()
        self.tabsCollectionView.reloadData()
    }
}

enum VerticalLocation: String {
    case bottom
    case top
}

extension FullScreenSlideshowViewController {
    
    open override func viewWillLayoutSubviews() {
        setAutoRotation(value: true)
        
        let navView = UIView()
        navView.frame = CGRect(x: 0, y: 0, width: view.frame.size.width, height: 64)
        
        super.viewWillLayoutSubviews()
    }
    
    open override func dismiss(animated flag: Bool, completion: (() -> Void)? = nil) {
        UIDevice.current.setValue(UIInterfaceOrientation.portrait.rawValue, forKey: "orientation")
        super.dismiss(animated: flag, completion: completion)
    }
    
}
